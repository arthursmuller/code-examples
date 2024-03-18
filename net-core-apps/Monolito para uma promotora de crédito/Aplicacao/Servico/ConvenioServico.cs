using Aplicacao.Model.Aeronautica;
using Aplicacao.Model.Convenio;
using Aplicacao.Model.InssEspecieBeneficio;
using Aplicacao.Model.Marinha;
using Aplicacao.Model.SiapeTipoFuncional;
using B.Mensagens;
using B.Mensagens.Interfaces;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Servico
{
    public class ConvenioServico : ServicoBase
    {
        public ConvenioServico(IBemMensagens mensagens, IUsuarioLogin usuarioLogin, PlataformaClienteContexto contexto) : base(mensagens, usuarioLogin, contexto) { }

        public async Task<IEnumerable<ConvenioModel>> ListarConvenios()
        {
            var convenios = await _contexto.Convenios
                .AsNoTracking()
                .OrderBy(c => c.Nome)
                .ToListAsync();

            if (convenios == null)
            {
                _mensagens.AdicionarErro(Mensagens.Convenio_NenhumEncontrato, EnumMensagemTipo.banco);

                return null;
            }

            return converterConveniosParaModel(convenios);
        }

        public async Task<IEnumerable<ConvenioOrgaoModel>> ListarOrgaosSiape(string termo)
        {
            var orgaosQuery = _contexto.ConvenioOrgaos
                .Include(o => o.UF)
                .AsNoTracking()
                .Where(o => o.IdConvenio.Equals(Convenio.SIAPE));

            if (!String.IsNullOrWhiteSpace(termo))
            {
                orgaosQuery = orgaosQuery.Where(orgao =>
                    orgao.Nome.Contains(termo)
                    || (orgao.UF != null ? orgao.UF.Nome.Contains(termo) || orgao.UF.Sigla.Contains(termo) : false)
                    || orgao.CNPJ.Contains(termo));
            }

            var orgaos = await orgaosQuery
                .OrderBy(o => o.Nome)
                .ToListAsync();

            if (orgaos.Count() == 0 && String.IsNullOrWhiteSpace(termo))
            {
                _mensagens.AdicionarAlerta(Mensagens.Convenio_NenhumOrgaoEncontrado, EnumMensagemTipo.banco);
                return null;
            }

            return orgaos.Select(orgao => new ConvenioOrgaoModel
            {
                Id = orgao.ID,
                Codigo = orgao.Codigo,
                Nome = orgao.Nome,
                CNPJ = orgao.CNPJ,
                UF = orgao.UF?.Nome ?? "",
            });
        }

        public async Task<ConvenioOrgaoModel> ObterOrgaoPorCodigo(string codigo)
        {
            var orgao = await _contexto.ConvenioOrgaos
                .Include(o => o.UF)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Codigo.Equals(codigo));

            if (orgao == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Convenio_NenhumOrgaoEncontrado, EnumMensagemTipo.banco);

                return new ConvenioOrgaoModel(); ;
            }

            return new ConvenioOrgaoModel
            {
                Id = orgao.ID,
                Codigo = orgao.Codigo,
                Nome = orgao.Nome,
                CNPJ = orgao.CNPJ,
                UF = orgao.UF?.Nome ?? "",
            };
        }

        public async Task<IEnumerable<SiapeTipoFuncionalModel>> ListarSiapeTiposFuncionais()
        {
            var opcoes = await _contexto.SiapeTiposFunacionais
                .AsNoTracking()
                .OrderBy(t => t.Descricao)
                .ToListAsync();

            if (opcoes == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Convenio_NenhumTipoFuncionalEncontrado, EnumMensagemTipo.banco);

                return null;
            }

            return opcoes.Select(opcao => new SiapeTipoFuncionalModel
            {
                Id = opcao.ID,
                Codigo = opcao.Codigo,
                Descricao = opcao.Descricao
            });
        }

        public async Task<IEnumerable<InssEspecieBeneficioModel>> ListarInssEspeciesBeneficios()
        {
            var opcoes = await _contexto.InssEspeciesBeneficios
                .AsNoTracking()
                .OrderBy(e => e.Descricao)
                .ToListAsync();

            if (opcoes == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Convenio_NenhumaEspecieBeneficioEncontrada, EnumMensagemTipo.banco);

                return null;
            }

            return opcoes.Select(opcao => new InssEspecieBeneficioModel
            {
                Id = opcao.ID,
                Codigo = opcao.Codigo,
                Descricao = opcao.Descricao
            });
        }

        public async Task<InssEspecieBeneficioModel> ObterInssEspecieBeneficioPorCodigo(string codigo)
        {
            var especieInss = await _contexto.InssEspeciesBeneficios.AsNoTracking().FirstOrDefaultAsync(e => e.Codigo.Equals(codigo));

            if (especieInss == null)
            {
                _mensagens.AdicionarAlerta(Mensagens.Convenio_NenhumaEspecieBeneficioEncontrada, EnumMensagemTipo.banco);

                return new InssEspecieBeneficioModel();
            }

            return new InssEspecieBeneficioModel
            {
                Id = especieInss.ID,
                Codigo = especieInss.Codigo,
                Descricao = especieInss.Descricao
            };
        }

        public async Task<IEnumerable<MarinhaTipoFuncionalModel>> ListarMarinhaTiposFuncionais()
        {
            var opcoes = await _contexto.MarinhaTiposFuncionais
                .AsNoTracking()
                .OrderBy(e => e.Descricao)
                .ToListAsync();

            if (opcoes == null || opcoes.Count() == 0)
            {
                _mensagens.AdicionarAlerta(Mensagens.Convenio_NenhumMarinhaTipoFuncionalEncontrado, EnumMensagemTipo.banco);
                return null;
            }

            return opcoes.Select(opcao => new MarinhaTipoFuncionalModel
            {
                Id = opcao.ID,
                Descricao = opcao.Descricao,
                Sigla = opcao.Sigla
            });
        }

        public async Task<IEnumerable<MarinhaCargosModel>> ListarMarinhaCargos()
        {
            var opcoes = await _contexto.MarinhaCargos
                .AsNoTracking()
                .OrderBy(e => e.Descricao)
                .ToListAsync();

            if (opcoes == null || opcoes.Count() == 0)
            {
                _mensagens.AdicionarAlerta(Mensagens.Convenio_NenhumMarinhaCargoEncontrado, EnumMensagemTipo.banco);
                return null;
            }

            return opcoes.Select(opcao => new MarinhaCargosModel
            {
                Id = opcao.ID,
                Descricao = opcao.Descricao,
                Sigla = opcao.Sigla,
                Codigo = opcao.Codigo
            });
        }

        public async Task<IEnumerable<AeronauticaTipoFuncionalModel>> ListarAeronauticaTiposFuncionais()
        {
            var opcoes = await _contexto.AeronauticaTiposFuncionais
                .AsNoTracking()
                .OrderBy(e => e.Descricao)
                .ToListAsync();

            if (opcoes == null || opcoes.Count() == 0)
            {
                _mensagens.AdicionarAlerta(Mensagens.Convenio_NenhumAeronauticaTipoFuncionalEncontrado, EnumMensagemTipo.banco);
                return null;
            }

            return opcoes.Select(opcao => new AeronauticaTipoFuncionalModel
            {
                Id = opcao.ID,
                Descricao = opcao.Descricao,
                Sigla = opcao.Sigla
            });
        }

        public async Task<IEnumerable<AeronauticaCargosModel>> ListarAeronauticaCargos()
        {
            var opcoes = await _contexto.AeronauticaCargos
                .AsNoTracking()
                .OrderBy(e => e.Descricao)
                .ToListAsync();

            if (opcoes == null || opcoes.Count() == 0)
            {
                _mensagens.AdicionarAlerta(Mensagens.Convenio_NenhumAeronauticaCargoEncontrado, EnumMensagemTipo.banco);
                return null;
            }

            return opcoes.Select(opcao => new AeronauticaCargosModel
            {
                Id = opcao.ID,
                Descricao = opcao.Descricao,
                Sigla = opcao.Sigla,
                Codigo = opcao.Codigo
            });
        }

        public static ConvenioModel ConverterParaModel(ConvenioDominio convenio)
        {
            return new ConvenioModel
            {
                ID = (int)convenio.ID,
                Nome = convenio.Nome,
                Codigo = convenio.Codigo,
                Descricao = convenio.Descricao
            };
        }

        private static IEnumerable<ConvenioModel> converterConveniosParaModel(List<ConvenioDominio> convenios)
        {
            return convenios.Select(x => ConverterParaModel(x));
        }
    }
}
