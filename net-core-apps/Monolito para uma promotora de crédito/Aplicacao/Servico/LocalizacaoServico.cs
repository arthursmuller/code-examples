using Aplicacao.CEP;
using Aplicacao.Model.Municipio;
using Aplicacao.Model.TipoLogradouro;
using Aplicacao.Model.UnidadeFederativa;
using B.Configuracao;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class LocalizacaoServico : ServicoBase, ILocalizacaoServico
    {
        private readonly Configuracao _configuracao;

        public LocalizacaoServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto, Configuracao configuracao) : base(mensagens, usuarioLogin, contexto)
            => _configuracao = configuracao;

        public async Task<IEnumerable<UnidadeFederativaModel>> ListarUnidadesFederativas()
        {
            var unidadesFederativas = await _contexto.UnidadesFederativas
                .AsNoTracking()
                .OrderBy(m => m.Nome)
                .ToListAsync();

            if (unidadesFederativas == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Localizacao_NenhumaUnidadeFederativaEcontrada, EnumMensagemTipo.banco);
                return null;
            }

            return unidadesFederativas.Select(i
                => new UnidadeFederativaModel
                {
                    Id = i.ID,
                    Nome = i.Nome,
                    Sigla = i.Sigla
                });
        }

        public async Task<IEnumerable<MunicipioModel>> ListarMunicipios(int idUF, string busca)
        {
            var municipios = await _contexto.Municipios
                                    .Include(i => i.UF)
                                    .Where(m => m.IdUnidadeFederativa.Equals(idUF) && (string.IsNullOrWhiteSpace(busca) || m.Descricao.Contains(busca)))
                                    .AsNoTracking()
                                    .OrderBy(m => m.Descricao)
                                    .ToListAsync();

            if (municipios == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Localizacao_NenhumMunicipioEncontrado, EnumMensagemTipo.banco);

                return null;
            }

            return municipios.Select(municipio => new MunicipioModel(municipio));
        }

        public async Task<MunicipioModel> ObterMunicipio(int idUF, string municipio)
        {
            var municipios = await _contexto.Municipios
                                    .Include(i => i.UF)
                                    .Where(m => m.IdUnidadeFederativa.Equals(idUF) && m.Descricao.Contains(municipio))
                                    .AsNoTracking()
                                    .OrderBy(m => m.Descricao)
                                    .ToListAsync();

            var quantidadeCidades = municipios?.Count() ?? 0;

            if (quantidadeCidades == 1)
                return new MunicipioModel(municipios.First());

            if (quantidadeCidades > 1)
            {
                var cidadesNomeExato = municipios.FirstOrDefault(m => m.Descricao == municipio);

                if (cidadesNomeExato != null)
                    return new MunicipioModel(cidadesNomeExato);

                _mensagens.AdicionarAlerta(Mensagens.Localizacao_MunicipioNaoFoipossivelLocalizarApenasUm, EnumMensagemTipo.formulario);
            }

            if (quantidadeCidades == 0)
                _mensagens.AdicionarAlerta(Mensagens.Localizacao_NenhumMunicipioEncontrado, EnumMensagemTipo.banco);

            return null;
        }

        public async Task<IEnumerable<TipoLogradouroModel>> ListarTiposLogradouro(string descricao)
        {
            var logradouros = await _contexto.TiposLogradouro
                                    .Where(l => string.IsNullOrWhiteSpace(descricao) || l.Descricao.Contains(descricao))
                                    .AsNoTracking()
                                    .OrderBy(l => l.Descricao)
                                    .ToListAsync();

            if (logradouros == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Localizacao_NenhumLogradouroEncontrado, EnumMensagemTipo.banco);

                return null;
            }

            return logradouros.Select(tipo => new TipoLogradouroModel
            {
                Id = tipo.ID,
                Descricao = tipo.Descricao,
                Codigo = tipo.Codigo
            });
        }

        public async Task<TipoLogradouroModel> ObterTipoLogradouroPorCodigo(string codigo)
        {
            var tipoLogradouro = await _contexto.TiposLogradouro
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(t => t.Codigo.Equals(codigo));

            if (tipoLogradouro == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Localizacao_TipoLogradouroNaoEncontrado, EnumMensagemTipo.banco);

                return new TipoLogradouroModel();
            }

            return new TipoLogradouroModel
            {
                Id = tipoLogradouro.ID,
                Descricao = tipoLogradouro.Descricao,
                Codigo = tipoLogradouro.Codigo
            };
        }

        public async Task<CepModel> ObterLocalizacaoPorCEP(string descricao)
        {
            var localizacao = await _contexto.CEPs
                                    .Include(c => c.TipoLogradouro)
                                    .Include(c => c.Municipio)
                                        .ThenInclude(m => m.UF)
                                    .Include(c => c.UF)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.CEP.Equals(descricao));

            if (localizacao == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Localizacao_NenhumaEncontradaPeloCep, EnumMensagemTipo.banco);

                return null;
            }

            return new CepModel(localizacao);
        }

        public async Task<IEnumerable<CepModel>> BuscarCeps(CepEnvioModel parametros)
        {
            var quantidadeMaximaRetorno = int.Parse(_configuracao.BuscarParametro("cep-quantidade-maxima-retorno"));

            var localizacoes = await _contexto.CEPs
                                        .Include(c => c.TipoLogradouro)
                                        .Include(c => c.Municipio)
                                            .ThenInclude(m => m.UF)
                                        .Include(c => c.UF)
                                        .Where(c =>
                                            c.IdUnidadeFederativa.Equals(parametros.IdUnidadeFederativa)
                                            && c.IdMunicipio.Equals(parametros.IdMunicipio)
                                            && (!parametros.IdTipoLogradouro.HasValue || c.IdTipoLogradouro.Equals(parametros.IdTipoLogradouro.Value))
                                            && (string.IsNullOrWhiteSpace(parametros.Bairro) || c.Bairro.Contains(parametros.Bairro))
                                            && (string.IsNullOrWhiteSpace(parametros.Logradouro) || c.Logradouro.Contains(parametros.Logradouro))
                                        )
                                        .AsNoTracking()
                                        .Take(quantidadeMaximaRetorno)
                                        .OrderBy(c => c.TipoLogradouro.Descricao)
                                            .ThenBy(c => c.Logradouro)
                                        .ToListAsync();

            if (localizacoes == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Localizacao_NenhumCepLocalizado, EnumMensagemTipo.banco);

                return null;
            }

            return localizacoes.Select(localizacao => new CepModel(localizacao));
        }
    }
}
