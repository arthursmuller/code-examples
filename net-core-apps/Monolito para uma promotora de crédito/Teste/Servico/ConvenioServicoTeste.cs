using Aplicacao.Servico;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class ConvenioServicoTeste : ServicoTesteBase
    {
        private const decimal QUANTIDADE_CARGOS_MARINHA = 3;
        private const decimal QUANTIDADE_TIPOS_FUNCIONAIS_MARINHA = 2;
        private const decimal QUANTIDADE_CARGOS_AERONAUTICA = 4;
        private const decimal QUANTIDADE_TIPOS_FUNCIONAIS_AERONAUTICA = 2;

        private readonly ConvenioServico _convenioServico;

        public ConvenioServicoTeste() : base()
        {
            _convenioServico = new ConvenioServico(_mensagens, _usuarioLogin, _contexto);
        }

        [Fact]
        public async Task ListarOrgaosSiape_PorTermo_DeveRetornarContendoTermo()
        {
            var conveniosOrgaos = await criarConvenios();

            var resultadosPorNome = await _convenioServico.ListarOrgaosSiape(conveniosOrgaos.Last().Nome.Split(' ')[0]);

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.Equal(1, resultadosPorNome.Count());

            var resultadosPorNomeUF = await _convenioServico.ListarOrgaosSiape(conveniosOrgaos.First().UF.Nome.Split(' ')[0]);

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.Equal(1, resultadosPorNomeUF.Count());

            var resultadosPorSiglaUF = await _convenioServico.ListarOrgaosSiape(conveniosOrgaos.First().UF.Sigla);

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.Equal(1, resultadosPorSiglaUF.Count());

            var resultadosPorCNPJ = await _convenioServico.ListarOrgaosSiape(conveniosOrgaos.First().CNPJ);

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.Equal(1, resultadosPorCNPJ.Count());
        }

        [Fact]
        public async Task ListarOrgaosSiape_SemTermo_DeveRetornarTodos()
        {
            await criarConvenios();
            var resultado = await _convenioServico.ListarOrgaosSiape(null);

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task ListarOrgaosSiape_QuandoHaTermoSemEquivalente_DeveRetornarErro()
        {
            await criarConvenios();
            var resultado = await _convenioServico.ListarOrgaosSiape("non existing term");

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.Empty(resultado);
        }

        [Fact]
        public async Task ListarOrgaosSiape_QuandoNaoHa_DeveRetornarAlerta()
        {
            var resultado = await _convenioServico.ListarOrgaosSiape(null);

            Assert.NotEmpty(_mensagens.BuscarAlertas());
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ListarCargosMarinha_QuandoHa_DeveRetornarLista()
        {
            await criarMarinhaCargos();

            var resultado = await _convenioServico.ListarMarinhaCargos();

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.Equal(QUANTIDADE_CARGOS_MARINHA, resultado.Count());
        }

        [Fact]
        public async Task ListarCargosMarinha_QuandoNaoHa_DeveRetornarErro()
        {
            var resultado = await _convenioServico.ListarMarinhaCargos();

            Assert.NotEmpty(_mensagens.BuscarAlertas());
            Assert.Contains(_mensagens.BuscarAlertas(), erro => erro.Mensagem.Equals(Mensagens.Convenio_NenhumMarinhaCargoEncontrado));
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ListarTiposFuncionaisMarinha_QuandoHa_DeveRetornarLista()
        {
            await criarMarinhaTiposFuncionais();

            var resultado = await _convenioServico.ListarMarinhaTiposFuncionais();

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.Equal(QUANTIDADE_TIPOS_FUNCIONAIS_MARINHA, resultado.Count());
        }

        [Fact]
        public async Task ListarTiposFuncionaisMarinha_QuandoNaoHa_DeveRetornarErro()
        {
            var resultado = await _convenioServico.ListarMarinhaTiposFuncionais();

            Assert.NotEmpty(_mensagens.BuscarAlertas());
            Assert.Contains(_mensagens.BuscarAlertas(), erro => erro.Mensagem.Equals(Mensagens.Convenio_NenhumMarinhaTipoFuncionalEncontrado));
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ListarCargosAeronautica_QuandoHa_DeveRetornarLista()
        {
            await criarAeronauticaCargos();

            var resultado = await _convenioServico.ListarAeronauticaCargos();

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.Equal(QUANTIDADE_CARGOS_AERONAUTICA, resultado.Count());
        }

        [Fact]
        public async Task ListarCargosAeronautica_QuandoNaoHa_DeveRetornarErro()
        {
            var resultado = await _convenioServico.ListarAeronauticaCargos();

            Assert.NotEmpty(_mensagens.BuscarAlertas());
            Assert.Contains(_mensagens.BuscarAlertas(), erro => erro.Mensagem.Equals(Mensagens.Convenio_NenhumAeronauticaCargoEncontrado));
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ListarTiposFuncionaisAeronautica_QuandoHa_DeveRetornarLista()
        {
            await criarAeronauticaTiposFuncionais();

            var resultado = await _convenioServico.ListarAeronauticaTiposFuncionais();

            Assert.Empty(_mensagens.BuscarAlertas());
            Assert.Equal(QUANTIDADE_TIPOS_FUNCIONAIS_AERONAUTICA, resultado.Count());
        }

        [Fact]
        public async Task ListarTiposFuncionaisAeronautica_QuandoNaoHa_DeveRetornarErro()
        {
            var resultado = await _convenioServico.ListarAeronauticaTiposFuncionais();

            Assert.NotEmpty(_mensagens.BuscarAlertas());
            Assert.Contains(_mensagens.BuscarAlertas(), erro => erro.Mensagem.Equals(Mensagens.Convenio_NenhumAeronauticaTipoFuncionalEncontrado));
            Assert.Null(resultado);
        }

        private async Task<List<ConvenioOrgaoDominio>> criarConvenios()
        {
            var convenios = new List<ConvenioDominio> {
                new ConvenioDominio(Convenio.INSS, "INSS", "000020", ""),
                new ConvenioDominio(Convenio.SIAPE, "SIAPE", "002399", ""),
            };

            var ufs = new List<UnidadeFederativaDominio>()
            {
                new UnidadeFederativaDominio("Rio Grande do Sul", "RS"),
                new UnidadeFederativaDominio("Rondônia", "RO"),
                new UnidadeFederativaDominio("Roraima", "RR"),
                new UnidadeFederativaDominio("Santa Catarina", "SC"),
            };

            await _contexto.UnidadesFederativas.AddRangeAsync(ufs);
            await _contexto.Convenios.AddRangeAsync(convenios);
            await _contexto.SaveChangesAsync();

            var convenioOrgaos = new List<ConvenioOrgaoDominio>()
            {
                new ConvenioOrgaoDominio("20101", "00394411000109", "PRESIDENCIA DA REPUBLICA", Convenio.INSS, ufs[0].ID),
                new ConvenioOrgaoDominio("20102", "00894355000171", "VICE PRESIDENCIA DA REPUBLICA", Convenio.SIAPE, ufs[0].ID),
                new ConvenioOrgaoDominio("20104", "00394411001695", "SECRETARIA DE ASSUNTOS ESTRATEGICOS PR", Convenio.SIAPE, ufs[1].ID),
            };

            await _contexto.ConvenioOrgaos.AddRangeAsync(convenioOrgaos);
            await _contexto.SaveChangesAsync();

            return await _contexto.ConvenioOrgaos.Include(c => c.UF).ToListAsync();
        }

        private async Task criarMarinhaCargos()
        {
            var cargos = new List<MarinhaCargoDominio> {
                new MarinhaCargoDominio(1,"ALTE","Almirante"),
                new MarinhaCargoDominio(2,"AESQ","Almirante de Esquadra"),
                new MarinhaCargoDominio(3,"VALT","Vice Almirante"),
            };
            await _contexto.MarinhaCargos.AddRangeAsync(cargos);
            await _contexto.SaveChangesAsync();
        }

        private async Task criarAeronauticaCargos()
        {
            var cargos = new List<AeronauticaCargoDominio> {
                new AeronauticaCargoDominio(42,"MA","01 MARECHAL-DO-AR"),
                new AeronauticaCargoDominio(43,"TB","02 TENENTE-BRIGADEIRO"),
                new AeronauticaCargoDominio(44,"MB","03 MAJOR-BRIGADEIRO"),
                new AeronauticaCargoDominio(45,"BR","04 BRIGADEIRO"),
            };
            await _contexto.AeronauticaCargos.AddRangeAsync(cargos);
            await _contexto.SaveChangesAsync();
        }

        private async Task criarMarinhaTiposFuncionais()
        {
            var tiposFuncionais = new List<MarinhaTipoFuncionalDominio> {
                 new MarinhaTipoFuncionalDominio("S", "Servidor"),
                new MarinhaTipoFuncionalDominio("P", "Pensionista"),
            };
            await _contexto.MarinhaTiposFuncionais.AddRangeAsync(tiposFuncionais);
            await _contexto.SaveChangesAsync();
        }

        private async Task criarAeronauticaTiposFuncionais()
        {
            var tiposFuncionais = new List<AeronauticaTipoFuncionalDominio> {
                new AeronauticaTipoFuncionalDominio("S", "Servidor"),
                new AeronauticaTipoFuncionalDominio("P", "Pensionista"),
            };
            await _contexto.AeronauticaTiposFuncionais.AddRangeAsync(tiposFuncionais);
            await _contexto.SaveChangesAsync();
        }
    }
}
