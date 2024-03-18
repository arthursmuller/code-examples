using Aplicacao.Model.Cliente;
using Aplicacao.Model.Consignado;
using Aplicacao.Model.ParametroOperacao;
using Aplicacao.Servico;
using B.Mensagens;
using B.Mensagens.Implementacoes;
using Dominio;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Auth.Dto;
using Infraestrutura.Providers.Consignado;
using Infraestrutura.Providers.Consignado.Dto;
using Infraestrutura.Providers.Consignado.Dto.SimulacaoPortabilidade;
using Infraestrutura.Providers.Dto;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class ConsignadoServicoTeste : ServicoTesteBase
    {
        private const decimal PARCELA_COM_RESULTADO = 2000;

        private readonly Mock<IProviderConsignado> _providerConsignadoMock;
        private readonly ConsignadoServico _consignadoServico;

        public ConsignadoServicoTeste() : base()
        {
            _providerConsignadoMock = new Mock<IProviderConsignado>();
            var providerAutenticacaoMock = new Mock<IProviderAutenticacao>();
            var parametroOperacaoServicoMock = new Mock<IParametroOperacaoServico>();
            var clienteServicoMock = new Mock<IClienteServico>();

            parametroOperacaoServicoMock.Setup(service =>
                service.BuscarParametrosOperacao())
                    .ReturnsAsync(new List<ParametroOperacaoModel>() { new ParametroOperacaoModel() });

            providerAutenticacaoMock.Setup(service =>
                service.Autenticar(It.IsAny<ParametroAutenticacaoDto>()))
                    .ReturnsAsync(new RetornoAtenticacaoDto());

            _providerConsignadoMock.Setup(service =>
                service.SimularNovo(It.Is<ParametrosSimulacaoNovoDto>(param => param.Prestacao == PARCELA_COM_RESULTADO), It.IsAny<string>()))
                    .ReturnsAsync(new List<RetornoSimulacaoDto>() { new RetornoSimulacaoDto() });

            clienteServicoMock.Setup(service =>
                service.ObterClienteAutenticado())
                    .ReturnsAsync(new ClienteExibicaoModel());

            var configuracaoProviders = new ConfiguracaoProviders()
            {
                ConsignadoApi = "teste",
            };

            _consignadoServico = new ConsignadoServico(_mensagens, _usuarioLogin, _contexto, _providerConsignadoMock.Object, providerAutenticacaoMock.Object
                                    , parametroOperacaoServicoMock.Object, configuracaoProviders);
        }

        #region Simulação Novo

        [Fact]
        public async Task SimularNovo_QuandoInformadoValorPrestacaoEValorOperacao_DeveRetornarErro()
        {
            SimulacaoNovoEnvioModel requisicao = new SimulacaoNovoEnvioModel()
            {
                IdConvenio = 1,
                ValorOperacao = 200,
                ValorPrestacao = 200,
                RetornarSomenteOperacoesViaveis = true,
            };

            var resultado = await _consignadoServico.SimularNovo(requisicao);

            Assert.True(_mensagens.PossuiErros);
            Assert.True(_mensagens.TemErroFormulario);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task SimularNovo_QuandoNaoHouverResultados_DeveRetornarErroNegocio()
        {
            SimulacaoNovoEnvioModel requisicao = new SimulacaoNovoEnvioModel()
            {
                IdConvenio = 1,
                ValorPrestacao = 200,
                RetornarSomenteOperacoesViaveis = true,
            };

            var resultado = await _consignadoServico.SimularNovo(requisicao);

            Assert.NotNull(_mensagens.BuscarAlertas());
            Assert.Null(resultado);
        }

        [Fact]
        public async Task SimularNovo_QuandoHaResultados_DeveRetornar()
        {
            SimulacaoNovoEnvioModel requisicao = new SimulacaoNovoEnvioModel()
            {
                IdConvenio = 1,
                ValorPrestacao = PARCELA_COM_RESULTADO,
                RetornarSomenteOperacoesViaveis = true,
            };

            var resultado = await _consignadoServico.SimularNovo(requisicao);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
        }

        #endregion

        #region Listar Contratos Cliente

        [Fact]
        public async Task ListarContratosCliente_QuandoUsuarioNaoLogado_DeveRetornarNull()
        {
            var contratos = await _consignadoServico.ListarContratosCliente(new ContratoClienteEnvioModel());

            Assert.Null(contratos);
        }

        [Fact]
        public async Task ListarContratosCliente_QuandoNaoPossuiAutorizacaoParaimportacao_DeveRetornarNullComMensagemDeAlerta()
        {
            await CriarUsuarioTesteAsync();

            var contratos = await _consignadoServico.ListarContratosCliente(new ContratoClienteEnvioModel());

            Assert.Null(contratos);
            Assert.False(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarAlertas(), alerta => alerta.Mensagem.Equals(Mensagens.Cliente_NaoHaRegistrodeAutorizacaoParaImportacaoDeDados));
        }

        [Fact]
        public async Task ListarContratosCliente_QuandoNaoHouverResultados_DeveRetornarVazio()
        {
            await CriarUsuarioTesteAsync();

            await atualizarAutorizacaoImportacaoDados();

            var contratos = await _consignadoServico.ListarContratosCliente(new ContratoClienteEnvioModel());

            Assert.False(_mensagens.PossuiErros);
            Assert.Empty(contratos);
        }

        [Fact]
        public async Task ListarContratosCliente_QuandoHouverResultados_DeveRetornarDados()
        {
            await CriarUsuarioTesteAsync();

            await atualizarAutorizacaoImportacaoDados();

            var retornoContratos = new List<RetornoContratoClienteDto>
            {
                new RetornoContratoClienteDto(),
                new RetornoContratoClienteDto()
            };

            _providerConsignadoMock
                .Setup(service => service.ListarContratosCliente(It.IsAny<ParametrosContratoClienteDto>(), It.IsAny<string>()))
                .ReturnsAsync(retornoContratos);

            var contratos = await _consignadoServico.ListarContratosCliente(new ContratoClienteEnvioModel());

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(2, contratos.Count());
        }

        #endregion

        #region Simulação Refinanciamento

        [Fact]
        public async Task SimularRefinanciamento_QuandoUsuarioNaoLogado_DeveRetornarNull()
        {
            var simulacoes = await _consignadoServico.SimularRefinanciamento(new SimulacaoRefinanciamentoEnvioModel());

            Assert.Null(simulacoes);
        }

        [Fact]
        public async Task SimularRefinanciamento_QuandoNaoHouverResultados_DeveRetornarVazio()
        {
            await CriarUsuarioTesteAsync();

            var alerta = new MensagemBase(Mensagens.Simulacao_NaoRetornouResultadosComParametrosInformados, EnumMensagemTipo.negocio);

            var simulacoes = await _consignadoServico.SimularRefinanciamento(new SimulacaoRefinanciamentoEnvioModel { IdConvenio = 3 });

            Assert.False(_mensagens.PossuiErros);
            Assert.True(_mensagens.BuscarAlertas().Where(m => m.Mensagem.Equals(alerta.Mensagem) && m.Tipo.Equals(alerta.Tipo)).Any());
            Assert.Null(simulacoes);
        }

        [Fact]
        public async Task SimularRefinanciamento_QuandoHouverResultados_DeveRetornarItemComMaiorTaxaPorPlano()
        {
            await CriarUsuarioTesteAsync();

            (var mockRetornoSimulacao, var retornoEsperado) = montarDadosSimulacaoRetorno();

            _providerConsignadoMock
                .Setup(service => service.SimularRefinanciamento(It.IsAny<ParametrosSimulacaoRefinanciamentoDto>(), It.IsAny<string>()))
                .ReturnsAsync(mockRetornoSimulacao);

            var simulacoes = await _consignadoServico.SimularRefinanciamento(new SimulacaoRefinanciamentoEnvioModel { IdConvenio = 3 });

            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(4, simulacoes.Count());
            retornoEsperado.ForEach(r => Assert.Contains(simulacoes, s => s.Plano.Equals(r.Plano) && s.TaxaMes.Equals(r.TaxaMes)));
        }

        #endregion

        #region Simulação Portabilidade

        [Fact]
        public async Task SimularPortabilidade_QuandoUsuarioInvalido_DeveRetornarNullComErro()
        {
            var simulacoes = await _consignadoServico.SimularPortabilidade(new SimulacaoPortabilidadeEnvioModel { IdRendimentoCliente = 0 });

            Assert.Null(simulacoes);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.Usuario_NaoEncontrado));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        public async Task SimularPortabilidade_QuandoRendimentoInvalido_DeveRetornarNullComErro(int idRendimentoCliente)
        {
            await CriarUsuarioTesteAsync();
            await criarDadosRendimento();

            var simulacoes = await _consignadoServico.SimularPortabilidade(new SimulacaoPortabilidadeEnvioModel { IdRendimentoCliente = idRendimentoCliente });

            Assert.Null(simulacoes);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.Rendimento_NaoLocalizado));
        }

        [Fact]
        public async Task SimularPortabilidade_QuandoDadosValidos_DeveRetornarSimulacaoSemErros()
        {
            InstanciarAdapter();

            await CriarUsuarioTesteAsync();
            await criarDadosRendimento();

            (var mockRetornoSimulacao, var retornoEsperado) = montarDadosSimulacaoRetorno();
            var retornoSimulacaoPortabilidade = new RetornoSimulacaoPortabilidadeDto
            {
                Viabilidade = It.IsAny<ViabilidadePortabilidadeDto>(),
                Simulacao = mockRetornoSimulacao
            };

            _providerConsignadoMock
                .Setup(service => service.SimularPropostaPortabilidade(It.IsAny<ParametrosSimulacaoPortabilidadeDto>(), It.IsAny<string>()))
                .ReturnsAsync(retornoSimulacaoPortabilidade);

            var simulacao = await _consignadoServico.SimularPortabilidade(new SimulacaoPortabilidadeEnvioModel { IdRendimentoCliente = 1 });

            Assert.NotNull(simulacao);
            Assert.False(_mensagens.PossuiErros);
            Assert.Equal(4, simulacao.SimulacoesIntencaoRefinanciamento.Count());
            retornoEsperado.ForEach(r =>
                Assert.Contains(simulacao.SimulacoesIntencaoRefinanciamento, s => s.Plano.Equals(r.Plano) && s.TaxaMes.Equals(r.TaxaMes)));
        }

        #endregion

        private async Task atualizarAutorizacaoImportacaoDados()
        {
            var cliente = await _contexto.Clientes
                                            .FirstOrDefaultAsync(c => c.IdUsuario.Equals(_usuarioLogin.IdUsuario));
            cliente.SetAutorizacaoImportacaoDados(autorizacaoConcedida: true);
            await _contexto.SaveChangesAsync();
        }

        private (List<RetornoSimulacaoDto> mockRetorno, List<RetornoSimulacaoDto> retornoEsperado) montarDadosSimulacaoRetorno()
        {
            var mockRetornoSimulacao = new List<RetornoSimulacaoDto>
            {
                new RetornoSimulacaoDto{ Plano = "1", TaxaMes = (decimal)1.8 },
                new RetornoSimulacaoDto{ Plano = "1", TaxaMes = (decimal)1.7 },
                new RetornoSimulacaoDto{ Plano = "2", TaxaMes = (decimal)1.8 },
                new RetornoSimulacaoDto{ Plano = "3", TaxaMes = (decimal)1.5 },
                new RetornoSimulacaoDto{ Plano = "3", TaxaMes = (decimal)1.9 },
                new RetornoSimulacaoDto{ Plano = "3", TaxaMes = (decimal)1.6 },
                new RetornoSimulacaoDto{ Plano = "4", TaxaMes = (decimal)1.8 }
            };

            var retornoEsperado = new List<RetornoSimulacaoDto>
            {
                new RetornoSimulacaoDto{ Plano = "1", TaxaMes = (decimal)1.8 },
                new RetornoSimulacaoDto{ Plano = "2", TaxaMes = (decimal)1.8 },
                new RetornoSimulacaoDto{ Plano = "3", TaxaMes = (decimal)1.9 },
                new RetornoSimulacaoDto{ Plano = "4", TaxaMes = (decimal)1.8 }
            };

            return (mockRetornoSimulacao, retornoEsperado);
        }

        private async Task criarDadosRendimento()
        {
            await _contexto.AddAsync(new ConvenioDominio(Convenio.INSS, "INSS", "000020", ""));
            await _contexto.AddAsync(new ConvenioDominio(Convenio.SIAPE, "SIAPE", "000021", ""));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new ConvenioOrgaoDominio("00001", "00394411000109", "INSS", Convenio.INSS, null));
            await _contexto.AddAsync(new ConvenioOrgaoDominio("00002", "00394411000109", "SIAPE 1", Convenio.SIAPE, null));
            await _contexto.AddAsync(new ConvenioOrgaoDominio("00003", "00394411000109", "SIAPE 2", Convenio.SIAPE, null));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new UnidadeFederativaDominio("Rio Grande do Sul", "RS"));
            await _contexto.AddAsync(new TipoContaDominio(TipoConta.Normal, "Conta Bacana", "CB"));
            await _contexto.AddAsync(new BancoDominio("0012", "123456789", "Hehe Bank", false));
            await _contexto.AddAsync(new BancoDominio("12345", "1234567890", "Jajaja Bank", false));
            await _contexto.AddAsync(new SiapeTipoFuncionalDominio("S", "Servidor"));
            await _contexto.AddAsync(new InssEspecieBeneficioDominio("04", "Aposentadoria por invalidez do trabalhador rural (Lei Complementar no 11/71)"));
            await _contexto.AddAsync(new InssEspecieBeneficioDominio("05", "Aposentadoria por invalidez do trabalhador urbano (Lei Complementar no 11/71)"));
            await _contexto.SaveChangesAsync();

            await _contexto.AddAsync(new FormaRecebimentoDominio(FormaRecebimento.TED, "TED"));
            await _contexto.SaveChangesAsync();

            var usuario = await CriarUsuarioTesteAsync();
            var conta = new ContaClienteDominio(usuario.Cliente.ID, 1, TipoConta.Normal, "0001", "000001", FormaRecebimento.TED);
            await _contexto.AddAsync(conta);

            var rendimentos = new List<RendimentoClienteDominio> {
                new RendimentoClienteInssDominio(conta.ID, conta.ID, 1, Convenio.INSS, 1, 1, 1000, "0000000000", 1, DateTime.Now),
                new RendimentoClienteInssDominio(conta.ID, conta.ID, 2, Convenio.INSS, 1, 1, 1000, "0000000001", 1, DateTime.Now)
            };
            await _contexto.AddRangeAsync(rendimentos);
            await _contexto.SaveChangesAsync();
        }
    }
}
