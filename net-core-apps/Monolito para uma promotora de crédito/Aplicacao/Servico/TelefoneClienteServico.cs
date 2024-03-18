using Aplicacao.Model.TelefoneCliente;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers.Cliente.Dto;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class TelefoneClienteServico : ServicoBase
    {
        public TelefoneClienteServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto) : base(mensagens, usuarioLogin, contexto) { }

        public async Task<IEnumerable<TelefoneClienteExibicaoModel>> BuscarTelefonesPorCliente()
        {
            var cliente = await _contexto.Clientes
                .Include(cliente => cliente.TelefonePrincipal)
                .Include(cliente => cliente.TelefoneSecundario)
                .AsNoTracking()
                .FirstOrDefaultAsync(cliente => cliente.IdUsuario == _usuarioLogin.IdUsuario);

            if (cliente is null)
            {
                _mensagens.AdicionarErro(Mensagens.Cliente_NaoEncontrado, B.Mensagens.EnumMensagemTipo.banco);
                return null;
            }

            return new List<TelefoneClienteExibicaoModel>()
            {
                new TelefoneClienteExibicaoModel(cliente.TelefonePrincipal, true),
                new TelefoneClienteExibicaoModel(cliente.TelefoneSecundario)
            };
        }

        public async Task<bool> GravarTelefone(TelefoneClienteModel telefoneGravacao)
        {
            var cliente = await _contexto.Clientes
                .Include(cliente => cliente.Telefones)
                .FirstOrDefaultAsync(cliente => cliente.IdUsuario == _usuarioLogin.IdUsuario);

            if (cliente is null)
            {
                _mensagens.AdicionarErro(Mensagens.Cliente_NaoEncontrado, B.Mensagens.EnumMensagemTipo.banco);
                return false;
            }

            TelefoneClienteDominio telefone;

            if (telefoneGravacao.Id != null)
            {
                telefone = cliente.Telefones.FirstOrDefault(telefone => telefone.ID == telefoneGravacao.Id);

                if (telefone == null)
                {
                    _mensagens.AdicionarErro(Mensagens.Telefone_NaoLocalizado, EnumMensagemTipo.formulario);
                    return false;
                }

                telefone.SetAtualizarTelefone(new Fone(telefoneGravacao.DDD, telefoneGravacao.Fone));
            }
            else
            {
                telefone = new TelefoneClienteDominio(
                    cliente.ID,
                    new Fone(telefoneGravacao.DDD, telefoneGravacao.Fone)
                );

                await _contexto.AddAsync(telefone);
            }

            await _contexto.SaveChangesAsync();

            if (cliente.IdTelefonePrincipal == null || telefoneGravacao.Principal)
            {
                cliente.SetTelefonePrincipal(telefone.ID);
            }
            else
            {
                cliente.SetTelefoneSecundario(telefone.ID);
            }

            await _contexto.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletarTelefone(int idTelefone)
        {
            var telefone = await _contexto
                                    .TelefonesCliente
                                    .Include(t => t.Cliente)
                                    .Where(t => t.ID.Equals(idTelefone) && t.Cliente.IdUsuario.Equals(_usuarioLogin.IdUsuario))
                                    .FirstOrDefaultAsync();

            if (!validarSePermiteRemover(telefone))
                return false;

            telefone.AlternarDeletado(true);

            if (telefone.Cliente.IdTelefoneSecundario == telefone.ID)
                telefone.Cliente.SetTelefoneSecundario(default);

            await _contexto.SaveChangesAsync();

            return true;
        }

        private bool validarSePermiteRemover(TelefoneClienteDominio telefone)
        {
            if (telefone == null)
            {
                _mensagens.AdicionarErro(Mensagens.Telefone_NaoLocalizado, EnumMensagemTipo.negocio);
                return false;
            }
            else if (telefone.Cliente.IdTelefonePrincipal == telefone.ID)
            {
                _mensagens.AdicionarErro(Mensagens.Telefone_IndicadoComoPrincipalNaoPodeSerRemovido, EnumMensagemTipo.negocio);
                return false;
            }

            return true;
        }

        #region Importação

        public async Task ImportarTelefones(ClienteContatosDto contato)
        {
            var telefones = converterTelefones(contato);
            telefones = telefones.Where(t => new TelefoneClienteModelValidacao(_mensagens).Validate(t).IsValid);
            if (telefones.Any())
            {
                telefones.First().Principal = true;
                await GravarTelefone(telefones.First());

                if (telefones.Count() > 1)
                    await GravarTelefone(telefones.ElementAt(1));
            }
        }

        private static IEnumerable<TelefoneClienteModel> converterTelefones(ClienteContatosDto contato)
        {
            List<TelefoneClienteModel> telefones = new List<TelefoneClienteModel>();
            if (contato?.Telefone1 != null)
                telefones.Add(obterTelefoneConvertido(contato.Telefone1));

            if (contato?.Telefone2 != null)
                telefones.Add(obterTelefoneConvertido(contato.Telefone2));

            return telefones;
        }

        private static TelefoneClienteModel obterTelefoneConvertido(TelefoneDto telefone)
        {
            return new TelefoneClienteModel
            {
                DDD = telefone.DDD,
                Fone = telefone.Telefone
            };
        }

        #endregion
    }
}
