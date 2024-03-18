using Aplicacao.Servico;
using B.Configuracao;
using Dominio;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class LocalizacaoServicoTeste: ServicoTesteBase
    {
        private readonly LocalizacaoServico _localizacaoServico;

        public LocalizacaoServicoTeste(): base()
        {
            _localizacaoServico = new LocalizacaoServico(_mensagens, _usuarioLogin, _contexto, It.IsAny<Configuracao>());
        }

        [Fact]
        public async Task ListarMunicipios_ParaUmaUF_DeveRetornarMunicipiosRelacionados()
        {
            var (ufs, _) = await criarLocalizacoes();

            var resultado = await _localizacaoServico.ListarMunicipios(ufs.First().ID, null);

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task ListarMunicipios_ComTermoBusca_DeveRetornarMunicipiosRelacionadosContendoTermo()
        {
            var (ufs, municipios) = await criarLocalizacoes();

            var resultado = await _localizacaoServico.ListarMunicipios(ufs.First().ID, "Porto");

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(1, resultado.Count());
        }

        [Fact]
        public async Task ObterMunicipio_QuandoUmResultado_DeveRetornarMunicipio()
        {
            var (ufs, municipios) = await criarLocalizacoes();

            var resultado = await _localizacaoServico.ObterMunicipio(ufs.First().ID, "Porto");

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task ObterMunicipio_QuandoVariosResultados_DeveRetornarAlerta()
        {
            var (ufs, municipios) = await criarLocalizacoes();

            var resultado = await _localizacaoServico.ObterMunicipio(ufs.First().ID, "");

            Assert.Single(_mensagens.BuscarAlertas());
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ObterMunicipio_TendoNenhumResultado_DeveRetornarAlerta()
        {
            var (ufs, municipios) = await criarLocalizacoes();

            var resultado = await _localizacaoServico.ObterMunicipio(ufs.First().ID, "Unit Test City");

            Assert.Single(_mensagens.BuscarAlertas());
            Assert.Null(resultado);
        }

        private async Task<(List<UnidadeFederativaDominio>, List<MunicipioDominio>)> criarLocalizacoes()
        {
            var ufs = new List<UnidadeFederativaDominio>()
            {
                new UnidadeFederativaDominio("Rio Grande do Sul", "RS"),
                new UnidadeFederativaDominio("Santa Catarina", "SC"),
            };

            await _contexto.UnidadesFederativas.AddRangeAsync(ufs);
            await _contexto.SaveChangesAsync();

            var municipios = new List<MunicipioDominio>()
            {
                new MunicipioDominio("Porto Alegre", ufs.First().ID),
                new MunicipioDominio("Canoas", ufs.First().ID),
                new MunicipioDominio("Florianópolis", ufs.Last().ID),
                new MunicipioDominio("Outro Porto", ufs.Last().ID),
            };
            
            await _contexto.Municipios.AddRangeAsync(municipios);
            await _contexto.SaveChangesAsync();

            return (ufs, municipios);
        }
    }
}
