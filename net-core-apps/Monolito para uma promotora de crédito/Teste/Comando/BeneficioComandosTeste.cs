using Aplicacao;
using Aplicacao.Comando;
using Aplicacao.Model.Anexo;
using Aplicacao.Model.Beneficio;
using Aplicacao.Servico;
using Dominio;
using Dominio.Resource;
using Infraestrutura;
using Infraestrutura.Providers;
using Infraestrutura.Providers.Auth;
using Infraestrutura.Providers.Auth.Dto;
using Infraestrutura.Providers.Cliente;
using Infraestrutura.Providers.Cliente.Dto.NovaAutorizacao;
using Infraestrutura.Providers.Consignado;
using Infraestrutura.Providers.Consignado.Dto;
using Infraestrutura.Providers.Dto;
using Infraestrutura.Providers.Paperless;
using Infraestrutura.Providers.Paperless.Dto;
using Infraestrutura.Providers.Paperless.Dto.AssinaturaDocumento;
using Infraestrutura.Providers.Paperless.Dto.ReenvioToken;
using Microsoft.EntityFrameworkCore;
using Moq;
using SharedKernel.ValueObjects.v2;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teste.Servico;
using Xunit;

namespace Teste.Comando
{
    public class BeneficioComandosTeste : ServicoTesteBase
    {
        private BeneficioInssReenvioTokenCommand _reenvioTokenCommand;
        private BeneficioInssSolicitacaoAutorizacaoCommand _solicitacaoAutorizacaoCommand;
        private BeneficioInssValidacaoTokenCommand _validacaoTokenCommand;
        private ConfiguracaoProviders _configuracaoProviders;
        private UsuarioDominio _usuarioTeste;
        private Mock<IProviderAutenticacao> _providerAutenticacao = new Mock<IProviderAutenticacao>();
        private Mock<IProviderCliente> _providerCliente = new Mock<IProviderCliente>();
        private Mock<IProviderPaperless> _providerPapeless = new Mock<IProviderPaperless>();
        private Mock<IAnexoServico> _anexoServico = new Mock<IAnexoServico>();
        private Mock<IProviderConsignado> _providerConsignado = new Mock<IProviderConsignado>();
        private Mock<IProviderMaxMind> _providerMaxMind = new Mock<IProviderMaxMind>();

        public BeneficioComandosTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();

            _providerAutenticacao
                .Setup(s => s.Autenticar(It.IsAny<ParametroAutenticacaoDto>()))
                .ReturnsAsync(new RetornoAtenticacaoDto());

            _configuracaoProviders = new ConfiguracaoProviders { Paperless = new ConfiguracaoProviderPaperless() };
        }

        [Fact]
        public async Task SolicitarAutorizacaoConsultaBeneficioInss_QuandoRetornoArquivoNull_DeveRetornarErro()
        {
            #region Arrange

            _providerConsignado
                .Setup(s => s.ObterTermoAutorizacaoBeneficiario(It.IsAny<ParametrosAutorizacaoBeneficiarioDto>(), It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<byte[]>());

            instanciarSolicitacaoAutorizacaoCommand();

            #endregion

            #region Act

            var autorizacao = await _solicitacaoAutorizacaoCommand.SolicitarAutorizacaoConsultaBeneficioInss(It.IsAny<SolicitacaoAutorizacaoConsultaBeneficioEnvioModel>());

            #endregion

            #region Assert

            Assert.Null(autorizacao);
            Assert.Single(_mensagens.BuscarErros());
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Beneficio_NaoFoiPossivelObterArquivoTermoAutorizacao));

            #endregion
        }

        [Fact]
        public async Task SolicitarAutorizacaoConsultaBeneficioInss_QuandoRetornoAssinaturaTermoInvalido_DeveRetornarErro()
        {
            #region Arrange

            await criarDadosEndereco();
            await criarDadosTelefone();

            _providerConsignado
                .Setup(s => s.ObterTermoAutorizacaoBeneficiario(It.IsAny<ParametrosAutorizacaoBeneficiarioDto>(), It.IsAny<string>()))
                .ReturnsAsync(new byte[] { });

            _providerPapeless
                .Setup(s => s.EnviarDocumentoParaAssinatura(It.IsAny<InformacoesEnvioDocumentoParaAssinaturaDto>(), It.IsAny<string>()))
                .ReturnsAsync(0);

            instanciarSolicitacaoAutorizacaoCommand();

            #endregion

            #region Act

            var parametrosSolicitacao = new SolicitacaoAutorizacaoConsultaBeneficioEnvioModel { IdTelefoneEnvioSolicitacao = 1 };
            var autorizacao = await _solicitacaoAutorizacaoCommand.SolicitarAutorizacaoConsultaBeneficioInss(parametrosSolicitacao);

            #endregion

            #region Assert

            Assert.Null(autorizacao);
            Assert.Single(_mensagens.BuscarErros());
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Beneficio_NaoHouveRetornoValidoProvedorAssinaturaTermoInss));

            #endregion
        }

        [Fact]
        public async Task SolicitarAutorizacaoConsultaBeneficioInss_QuandoDadosValidos_DeveRegistrarSemErros()
        {
            #region Arrange

            await criarDadosEndereco();
            await criarDadosTelefone();

            _providerConsignado
                .Setup(s => s.ObterTermoAutorizacaoBeneficiario(It.IsAny<ParametrosAutorizacaoBeneficiarioDto>(), It.IsAny<string>()))
                .ReturnsAsync(new byte[] { });

            _providerPapeless
                .Setup(s => s.EnviarDocumentoParaAssinatura(It.IsAny<InformacoesEnvioDocumentoParaAssinaturaDto>(), It.IsAny<string>()))
                .ReturnsAsync(1);

            instanciarSolicitacaoAutorizacaoCommand();

            #endregion

            #region Act

            var parametrosSolicitacao = new SolicitacaoAutorizacaoConsultaBeneficioEnvioModel { IdTelefoneEnvioSolicitacao = 1 };
            var autorizacao = await _solicitacaoAutorizacaoCommand.SolicitarAutorizacaoConsultaBeneficioInss(parametrosSolicitacao);
            var logConsultabeneficio = await obterLogConsultaBeneficioInss();

            #endregion

            #region Assert

            Assert.NotNull(autorizacao);
            Assert.True(autorizacao.IdConsultaBeneficio > 0);
            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(logConsultabeneficio);
            Assert.Equal(1, logConsultabeneficio.IdCliente);
            Assert.Equal(1, logConsultabeneficio.IdPaperlessDocumento);
            Assert.Null(logConsultabeneficio.ChaveAutorizacao);

            #endregion
        }

        [Fact]
        public async Task ReenviarTokenParaAssinatura_QuandoConsultaNaoLocalizada_DeveRetornarErro()
        {
            #region Arrange

            await criarDadosTelefone();

            instanciarReenvioTokenCommand();

            #endregion

            #region Act

            var parametrosReenvio = new SolicitacaoReenvioTokenAssinaturaModel { IdConsultaBeneficio = 1, IdTelefoneEnvioSolicitacao = 2 };
            var reenvioTokenExecutado = await _reenvioTokenCommand.ReenviarTokenParaAssinatura(parametrosReenvio);

            #endregion

            #region Assert

            Assert.False(reenvioTokenExecutado);
            Assert.Single(_mensagens.BuscarErros());
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Beneficio_RegistroDeConsultaDeBeneficioNaoLocalizada));

            #endregion
        }

        [Fact]
        public async Task ReenviarTokenParaAssinatura_QuandoTelefoneNaoEhCelular_DeveRetornarErro()
        {
            #region Arrange

            await criarDadosTelefone();
            await criarDadosConsultaBeneficioInss();

            instanciarReenvioTokenCommand();

            #endregion

            #region Act

            var parametrosReenvio = new SolicitacaoReenvioTokenAssinaturaModel { IdConsultaBeneficio = 1, IdTelefoneEnvioSolicitacao = 2 };
            var reenvioTokenExecutado = await _reenvioTokenCommand.ReenviarTokenParaAssinatura(parametrosReenvio);

            #endregion

            #region Assert

            Assert.False(reenvioTokenExecutado);
            Assert.Single(_mensagens.BuscarErros());
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Beneficio_TelefoneDeveSerCelular));

            #endregion
        }

        [Fact]
        public async Task ReenviarTokenParaAssinatura_QuandoDadosValidos_RetornoTrueSemErros()
        {
            #region Arrange

            await criarDadosTelefone();
            await criarDadosConsultaBeneficioInss();

            _providerPapeless
                .Setup(s => s.ReenviarToken(It.IsAny<ReenvioTokenParametrosDto>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            instanciarReenvioTokenCommand();

            #endregion

            #region Act

            var parametrosReenvio = new SolicitacaoReenvioTokenAssinaturaModel { IdConsultaBeneficio = 1, IdTelefoneEnvioSolicitacao = 1 };
            var reenvioTokenExecutado = await _reenvioTokenCommand.ReenviarTokenParaAssinatura(parametrosReenvio);

            #endregion

            #region Assert

            Assert.True(reenvioTokenExecutado);
            Assert.False(_mensagens.PossuiErros);

            #endregion
        }

        [Fact]
        private async Task ValidarTokenAssinatura_LogBeneficioInexistente_DeveRetornarNullComErro()
        {
            #region Arrange

            instanciarValidacaoTokenCommand();

            #endregion

            #region Act

            var parametros = new ValidacaoTokenAssinaturaEnvioModel { IdConsultaBeneficio = 1, TokenConsulta = "1234" };
            var resultadoValidacao = await _validacaoTokenCommand.ValidarTokenAssinatura(parametros);

            #endregion

            #region Assert

            Assert.Null(resultadoValidacao);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.Beneficio_RegistroDeConsultaDeBeneficioNaoLocalizada));

            #endregion
        }

        [Fact]
        public async Task ValidarTokenAssinatura_SemRetornoDeDocumentoAssinado_DeveRetornarErro()
        {
            #region Arrange

            await criarDadosConsultaBeneficioInss();

            _providerPapeless
                .Setup(s => s.AssinarDocumento(It.IsAny<AssinaturaDocumentoDto>(), It.IsAny<string>()))
                .ReturnsAsync(new byte[0]);

            instanciarValidacaoTokenCommand();

            #endregion

            #region Act

            var parametros = new ValidacaoTokenAssinaturaEnvioModel { IdConsultaBeneficio = 1, TokenConsulta = "1234" };
            var beneficios = await _validacaoTokenCommand.ValidarTokenAssinatura(parametros);

            #endregion

            #region Assert

            Assert.Null(beneficios);
            Assert.Single(_mensagens.BuscarErros());
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Beneficio_NaoFoiPossivelObterTermoAssinado));

            #endregion
        }

        [Fact]
        public async Task ValidarTokenAssinatura_AnexoRetornoInvalido_DeveRetornarErro()
        {
            #region Arrange

            await criarDadosConsultaBeneficioInss();

            _providerPapeless
                .Setup(s => s.AssinarDocumento(It.IsAny<AssinaturaDocumentoDto>(), It.IsAny<string>()))
                .ReturnsAsync(new byte[1]);

            _anexoServico
                .Setup(s => s.GravarArquivo(It.IsAny<AnexoCriacaoModel>()))
                .ReturnsAsync(It.IsAny<AnexoModel>());

            instanciarValidacaoTokenCommand();

            #endregion

            #region Act

            var parametros = new ValidacaoTokenAssinaturaEnvioModel { IdConsultaBeneficio = 1, TokenConsulta = "1234" };
            var beneficios = await _validacaoTokenCommand.ValidarTokenAssinatura(parametros);

            #endregion

            #region Assert

            Assert.Null(beneficios);
            Assert.Single(_mensagens.BuscarErros());
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Beneficio_NaoHouveRetornoValidoAoAnexarTermoAssinado));

            #endregion
        }

        [Fact]
        public async Task ConsultarBeneficiosInss_NovaChaveAutorizacaoConsultaInvalida_DeveRetornarErro()
        {
            #region Arrange

            await criarDadosConsultaBeneficioInss();

            _providerPapeless
                .Setup(s => s.AssinarDocumento(It.IsAny<AssinaturaDocumentoDto>(), It.IsAny<string>()))
                .ReturnsAsync(new byte[1]);

            _anexoServico
                .Setup(s => s.GravarArquivo(It.IsAny<AnexoCriacaoModel>()))
                .ReturnsAsync(new AnexoModel());

            _providerCliente
                .Setup(s => s.ObterNovaAutorizacaoParaConsultaBeneficioInss(It.IsAny<NovaAutorizacaoParametrosDto>(), It.IsAny<string>()))
                .ReturnsAsync(string.Empty);

            instanciarValidacaoTokenCommand();

            #endregion

            #region Act

            var parametros = new ValidacaoTokenAssinaturaEnvioModel { IdConsultaBeneficio = 1, TokenConsulta = "1234" };
            var beneficios = await _validacaoTokenCommand.ValidarTokenAssinatura(parametros);

            #endregion

            #region Assert

            Assert.Null(beneficios);
            Assert.Single(_mensagens.BuscarErros());
            Assert.Contains(_mensagens.BuscarErros(), e => e.Mensagem.Equals(Mensagens.Beneficio_NaoHouveRetornoValidoNovaAutorizacaoConsultaBeneficio));

            #endregion
        }

        private void instanciarReenvioTokenCommand()
            => _reenvioTokenCommand = new BeneficioInssReenvioTokenCommand(_mensagens, _usuarioLogin, _contexto, _providerPapeless.Object, _providerAutenticacao.Object, _configuracaoProviders);

        private void instanciarSolicitacaoAutorizacaoCommand()
            => _solicitacaoAutorizacaoCommand = new BeneficioInssSolicitacaoAutorizacaoCommand(_mensagens, _usuarioLogin, _contexto, _providerPapeless.Object, _providerConsignado.Object,
                _providerAutenticacao.Object, _providerMaxMind.Object, _configuracaoProviders);

        private void instanciarValidacaoTokenCommand()
            => _validacaoTokenCommand = new BeneficioInssValidacaoTokenCommand(_mensagens, _usuarioLogin, _contexto, _providerPapeless.Object, _providerAutenticacao.Object, _anexoServico.Object,
                _providerCliente.Object, _configuracaoProviders);

        private async Task<ConsultaBeneficioInssClienteDominio> obterLogConsultaBeneficioInss()
        {
            return await _contexto.ConsultaBeneficiosInssCliente
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.ID.Equals(1));
        }

        private async Task criarDadosConsultaBeneficioInss()
        {
            await _contexto.ConsultaBeneficiosInssCliente.AddAsync(new ConsultaBeneficioInssClienteDominio(_usuarioTeste.Cliente.ID, 1));
            await _contexto.SaveChangesAsync();
        }

        private async Task criarDadosEndereco()
        {
            await _contexto.UnidadesFederativas.AddAsync(new UnidadeFederativaDominio("Rio Grande do Sul", "RS"));
            await _contexto.SaveChangesAsync();

            await _contexto.Municipios.AddAsync(new MunicipioDominio("Porto Alegre", 1));
            await _contexto.TiposLogradouro.AddAsync(new TipoLogradouroDominio("Rua", "R"));
            await _contexto.SaveChangesAsync();

            var enderecos = new List<EnderecoClienteDominio>()
            {
                new EnderecoClienteDominio(_usuarioTeste.Cliente.ID, "Teste 1", 1, "centro", 1, null, 100, null, "90000111", true),
                new EnderecoClienteDominio(_usuarioTeste.Cliente.ID, "Teste 2", 1, "centro", 1, null, 200, null, "90000222", false),
            };

            await _contexto.EnderecosCliente.AddRangeAsync(enderecos);
            await _contexto.SaveChangesAsync();
        }

        private async Task criarDadosTelefone()
        {
            var telefones = new List<TelefoneClienteDominio>()
            {
                new TelefoneClienteDominio(_usuarioTeste.Cliente.ID, new Fone("51", "965874968")),
                new TelefoneClienteDominio(_usuarioTeste.Cliente.ID, new Fone("51", "39658754"))
            };

            await _contexto.TelefonesCliente.AddRangeAsync(telefones);
            await _contexto.SaveChangesAsync();
        }
    }
}
