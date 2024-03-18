using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Geolocalizacao;
using Infraestrutura.RedesSociais;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class LojaServico : ServicoBase
    {
        private readonly RedesSociaisConfiguracao _redesSociaisConfiguracao;
        private readonly GeolocalizacaoConfiguracao _geolocalizacaoConfiguracao;

        public LojaServico(
            PlataformaClienteContexto contexto,
            IBemMensagens mensagens,
            IUsuarioLogin usuarioLogin,
            RedesSociaisConfiguracao redesSociaisConfiguracao,
            GeolocalizacaoConfiguracao geolocalizacaoConfiguracao) : base(mensagens, usuarioLogin, contexto)
        {
            _redesSociaisConfiguracao = redesSociaisConfiguracao;
            _geolocalizacaoConfiguracao = geolocalizacaoConfiguracao;
        }

        public async Task<LojaModel> BuscarLoja(int id)
        {
            var loja = await queryLojas()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (loja == null)
            {
                _mensagens.AdicionarErro(string.Format(Mensagens.Loja_IdNaoEncontrada, id), EnumMensagemTipo.banco);
                return null;
            }

            return new LojaModel(loja);
        }

        public async Task<IEnumerable<LojaModel>> BuscarLojas(bool necessarioContatoWhatsApp = false, int? idUf = null)
        {
            var query = queryLojas().AsNoTracking();

            if (necessarioContatoWhatsApp)
                query = query.Where(l => l.Telefones.Any(a => a.PossuiContaWhatsApp));

            if (idUf.HasValue)
                query = query.Where(l => l.Municipio.IdUnidadeFederativa == idUf);

            var lojas = await query
                .OrderBy(l => l.Nome)
                .ToListAsync();

            if (lojas == null || !lojas.Any())
            {
                _mensagens.AdicionarAlerta(Mensagens.Loja_NenhumaEncontrada, EnumMensagemTipo.banco);

                return new List<LojaModel>();
            }

            return lojas.Select(loja => new LojaModel(loja));
        }

        public async Task<LojaModel> CriarLoja(LojaCriacaoModel requisicao)
        {
            List<TelefoneLojaDominio> telefones = new List<TelefoneLojaDominio>();
            if (requisicao.Telefones.Any())
            {
                telefones.AddRange(
                    requisicao.Telefones.Select(telefone => new TelefoneLojaDominio(telefone.Telefone, telefone.PossuiContaWhatsApp, telefone.MensagemApresentacao))
                );
            }

            var novaLoja = await _contexto.AddAsync(
                new LojaDominio(
                    requisicao.Nome,
                    requisicao.IdMunicipio,
                    requisicao.Bairro,
                    requisicao.IdTipoLogradouro,
                    requisicao.Logradouro,
                    requisicao.Numero,
                    requisicao.Complemento,
                    requisicao.Cep,
                    requisicao.MensagemApresentacao,
                    telefones,
                    new Point(requisicao.Longitude, requisicao.Latitude) { SRID = _geolocalizacaoConfiguracao.IdReferenciaEspacial }
                )
            );

            await _contexto.SaveChangesAsync();

            var loja = await queryLojas()
                .FirstOrDefaultAsync(l => l.ID == novaLoja.Entity.ID);

            return new LojaModel(loja);
        }

        public async Task<bool> DeletarLoja(int id)
        {
            var loja = await _contexto.Lojas
                .Include(l => l.Telefones)
                .FirstOrDefaultAsync(x => x.ID.Equals(id));

            if (loja == null)
            {
                _mensagens.AdicionarErro(string.Format(Mensagens.Loja_IdNaoEncontrada, id), EnumMensagemTipo.banco);
                return false;
            }

            _contexto.Remove(loja);

            await _contexto.SaveChangesAsync();

            return true;
        }

        public async Task<(int?, string)> ObterLinkContatoWhatsApp(int? idLoja, double latitude, double longitude)
        {
            var loja = await obterLojaEnvioWhatsApp(idLoja, latitude, longitude);

            if (loja == null)
            {
                return (null, null);
            }

            var telefoneWhatsApp = loja.Telefones != null ? loja.Telefones.FirstOrDefault(l => l.PossuiContaWhatsApp) : null;

            if (telefoneWhatsApp == null)
                return (idLoja, null);

            var mensagem = string.IsNullOrWhiteSpace(telefoneWhatsApp.MensagemApresentacao)
                            ? loja.MensagemApresentacao
                            : telefoneWhatsApp.MensagemApresentacao ?? "";

            return
            (
                loja.Id,
                string.Format
                (
                    _redesSociaisConfiguracao.WhatsApp.LinkGeradorFormatado,
                    telefoneWhatsApp.Telefone,
                    mensagem
                )
            );
        }

        private async Task<LojaModel> obterLojaEnvioWhatsApp(int? idLoja, double latitude, double longitude)
        {
            if (idLoja.HasValue)
                return await BuscarLoja(idLoja.Value);

            if (possuiLocalizacao(latitude, longitude))
                return new LojaModel(await buscarLojaProximaPelaGeolocalizacao(latitude, longitude, necessarioContatoWhatsApp: true));

            return await buscarLojaAleatoria();
        }

        private static bool possuiLocalizacao(double latitude, double longitude)
        {
            return latitude != 0 && longitude != 0;
        }

        private async Task<LojaModel> buscarLojaAleatoria()
        {
            var quantidadeLojas = await _contexto.Lojas.CountAsync();

            var quantidadeIgnorar = new Random().Next(quantidadeLojas);

            var loja = await queryLojas()
                .OrderBy(o => o.ID)
                .Skip(quantidadeIgnorar)
                .Take(1)
                .FirstAsync();

            return new LojaModel(loja);
        }

        private async Task<LojaDominio> buscarLojaProximaPelaGeolocalizacao(double latitude, double longitude, bool necessarioContatoWhatsApp = false)
        {
            var localizacaoReferencia = new Point(latitude, longitude) { SRID = _geolocalizacaoConfiguracao.IdReferenciaEspacial };
            var query = queryLojas()
                .AsNoTracking()
                .OrderBy(l => l.Geolocalizacao.Distance(localizacaoReferencia));

            if (necessarioContatoWhatsApp)
                return await query.FirstOrDefaultAsync(l => l.Telefones.Any(a => a.PossuiContaWhatsApp));

            return await query.FirstOrDefaultAsync();
        }

        private IQueryable<LojaDominio> queryLojas() => _contexto
            .Lojas
                .Include(l => l.Telefones)
                .Include(l => l.TipoLogradouro)
                .Include(l => l.Municipio)
                    .ThenInclude(m => m.UF);
    }
}
