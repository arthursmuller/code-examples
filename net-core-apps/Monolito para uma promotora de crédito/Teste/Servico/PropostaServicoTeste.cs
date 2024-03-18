using Aplicacao.Interfaces;
using Aplicacao.Servico;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Auth.Dto;
using Infraestrutura.Providers.BemApi;
using Infraestrutura.Providers.BemApi.Dto;
using Infraestrutura.Providers.Dto;
using Moq;
using SharedKernel.ValueObjects.v2;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class PropostaServicoTeste : ServicoTesteBase
    {
        private IPropostaServico _propostaServico;
        private IProviderBemApi _providerBemApi = new Mock<IProviderBemApi>().Object;
        private IProviderAutenticacao _providerAutenticacao = new Mock<IProviderAutenticacao>().Object;

        [Theory]
        [InlineData("12345")]
        [InlineData("12345123456")]
        public async Task ObterSituacaoProposta_EntradaComCpfInvalido_DeveRetornarNullComErro(string cpf)
        {
            instanciarPropostaServico();

            var situacaoProposta = await _propostaServico.ObterSituacaoProposta(cpf, It.IsAny<string>(), It.IsAny<DateTime>());

            Assert.Null(situacaoProposta);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.ToLower().Contains("cpf"));
        }

        [Fact]
        public async Task ObterSituacaoProposta_EntradaComDadosValidos_DeveRetornarResultadoSemErros()
        {
            InstanciarAdapter();

            var providerBemApiMock = new Mock<IProviderBemApi>();
            providerBemApiMock
                .Setup(s => s.ObterSituacaoProposta(It.IsAny<CPF>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>()))
                .ReturnsAsync(new ObtencaoSituacaoPropostaDto());
            _providerBemApi = providerBemApiMock.Object;

            var providerAutenticacaoMock = new Mock<IProviderAutenticacao>();
            providerAutenticacaoMock
                .Setup(s => s.Autenticar(It.IsAny<ParametroAutenticacaoDto>()))
                .ReturnsAsync(new RetornoAtenticacaoDto());
            _providerAutenticacao = providerAutenticacaoMock.Object;

            instanciarPropostaServico();

            var situacaoProposta = await _propostaServico.ObterSituacaoProposta("01234567890", It.IsAny<string>(), It.IsAny<DateTime>());

            Assert.NotNull(situacaoProposta);
            Assert.False(_mensagens.PossuiErros);
        }

        private void instanciarPropostaServico()
            => _propostaServico = new PropostaServico(_mensagens, _usuarioLogin, _contexto, _providerBemApi, _providerAutenticacao, It.IsAny<ConfiguracaoProviders>());
    }
}
