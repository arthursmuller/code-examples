using Aplicacao.Model.Autenticacao;
using Aplicacao.Servico;
using B.Autenticacao;
using Dominio.Enum;
using Dominio.Resource;
using Infraestrutura.Autenticacao;
using Infraestrutura.RedesSociais;
using LoginSocialApple;
using LoginSocialFacebook;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class AutenticacaoLoginSocialServicoTeste : ServicoTesteBase
    {
        private const string TOKEN = "12345";
        private AutenticacaoLoginSocialServico _autenticacaoLoginSocialServico;
        private Mock<IProvedorFacebook> _provedorFacebookMock = new Mock<IProvedorFacebook>();
        private Mock<IProvedorApple> _provedorAppleMock = new Mock<IProvedorApple>();
        private ILogger<IAutenticacaoLoginSocialServico> _loggerMock = new Mock<ILogger<IAutenticacaoLoginSocialServico>>().Object;
        private readonly string _tokenTeste = "FakeToken";
        private readonly string _appleCodeTeste = "FakeToken";
        private readonly string _redirectURLTeste = "FakeToken";


        [Fact]
        public async Task Autenticar_RedeSocialNaoContemplada_DeveRetornarNullComErro()
        {
            var loginSocial = new LoginSocialModel(0, TOKEN, null, null);
            instanciarAutenticacaoLoginSocialServico();

            var autenticacao = await _autenticacaoLoginSocialServico.Autenticar(loginSocial);

            Assert.Null(autenticacao);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.LoginSocial_NaoImplementado));
        }

        [Fact]
        public async Task Autenticar_LoginGoogleInvalido_DeveRetornarNullComErro()
        {
            var loginSocial = new LoginSocialModel(RedeSocial.Google, TOKEN);
            var logger = new Logger<IAutenticacaoLoginSocialServico>(new LoggerFactory());
            instanciarAutenticacaoLoginSocialServico();
            var autenticacao = await _autenticacaoLoginSocialServico.Autenticar(loginSocial);

            Assert.Null(autenticacao);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.LoginSocial_TokenInvalido));
        }

        [Fact]
        public async Task Autenticar_LoginFacebookInvalido_DeveRetornarNullComErro()
        {
            var loginSocial = new LoginSocialModel(RedeSocial.Facebook, TOKEN);

            _provedorFacebookMock
                .Setup(s => s.ValidarToken(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<ValidacaoTokenRetornoDto>());

            instanciarAutenticacaoLoginSocialServico();
            var autenticacao = await _autenticacaoLoginSocialServico.Autenticar(loginSocial);

            Assert.Null(autenticacao);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.LoginSocial_FacebookTokenInformadoInvalido));
        }

        [Fact]
        public async Task Autenticar_LoginSocialFacebookValido_DeveRetornarTokenAutenticacaoSemErros()
        {
            instanciarAutenticacaoLoginSocialServico();
            InstanciarAdapter();
            await criarDadosUsuarioLoginSocial(RedeSocial.Facebook);

            var loginSocial = new LoginSocialModel(RedeSocial.Facebook, TOKEN);

            _provedorFacebookMock
                .Setup(s => s.ValidarToken(It.IsAny<string>()))
                .ReturnsAsync(new ValidacaoTokenRetornoDto { Email = EMAIL_USUARIO_TESTE });

            var autenticacao = await _autenticacaoLoginSocialServico.Autenticar(loginSocial);

            Assert.NotNull(autenticacao);
            Assert.Equal(_tokenTeste, autenticacao.Token);
            Assert.Equal(EMAIL_USUARIO_TESTE, autenticacao.Email);
            Assert.True(autenticacao.UsuarioCadastrado);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task Autenticar_LoginAppleSignInInvalido_DeveRetornarNullComErro()
        {
            var loginSocial = new LoginSocialModel(RedeSocial.AppleSignIn, TOKEN, _appleCodeTeste, _redirectURLTeste);

            _provedorAppleMock
                .Setup(s => s.ValidarToken(It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<LoginSocialApple.Dto.ValidacaoTokenRetornoDto>());

            _provedorAppleMock
                .Setup(s => s.ValidarToken(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<LoginSocialApple.Dto.ValidacaoTokenRetornoDto>());

            instanciarAutenticacaoLoginSocialServico();
            var autenticacao = await _autenticacaoLoginSocialServico.Autenticar(loginSocial);

            Assert.Null(autenticacao);
            Assert.True(_mensagens.PossuiErros);
            Assert.Contains(_mensagens.BuscarErros(), erro => erro.Mensagem.Equals(Mensagens.LoginSocial_AppleTokenInformadoInvalido));
        }

        [Fact]
        public async Task Autenticar_LoginAppleSignInValido_DeveRetornarTokenAutenticacaoSemErros()
        {
            instanciarAutenticacaoLoginSocialServico();
            InstanciarAdapter();
            await criarDadosUsuarioLoginSocial(RedeSocial.AppleSignIn);

            var loginSocial = new LoginSocialModel(RedeSocial.AppleSignIn, TOKEN, _appleCodeTeste, _redirectURLTeste);

            _provedorAppleMock
                .Setup(s => s.ValidarToken(It.IsAny<string>()))
                .ReturnsAsync(new LoginSocialApple.Dto.ValidacaoTokenRetornoDto { Email = EMAIL_USUARIO_TESTE });
            _provedorAppleMock
                .Setup(s => s.ValidarToken(It.IsAny<string>(),It.IsAny<string>()))
                .ReturnsAsync(new LoginSocialApple.Dto.ValidacaoTokenRetornoDto { Email = EMAIL_USUARIO_TESTE });

            var autenticacao = await _autenticacaoLoginSocialServico.Autenticar(loginSocial);

            Assert.NotNull(autenticacao);
            Assert.Equal(_tokenTeste, autenticacao.Token);
            Assert.Equal(EMAIL_USUARIO_TESTE, autenticacao.Email);
            Assert.True(autenticacao.UsuarioCadastrado);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task Autenticar_LoginGoogleSignInValidoComUsuarioCriado_DeveRetornarTokenAutenticacaoSemErros()
        {
            instanciarAutenticacaoLoginSocialServico();
            InstanciarAdapter();
            var usuarioDominio = await CriarUsuarioTesteAsync();


            var loginSocial = new LoginSocialModel(RedeSocial.Facebook, TOKEN);

            _provedorFacebookMock
                .Setup(s => s.ValidarToken(It.IsAny<string>()))
                .ReturnsAsync( new ValidacaoTokenRetornoDto(){
                    Name = NOME_USUARIO_TESTE,
                    Email = EMAIL_USUARIO_TESTE,
                    Id = "1"
                });

            var autenticacao = await _autenticacaoLoginSocialServico.Autenticar(loginSocial);

            Assert.NotNull(autenticacao);
            Assert.Equal(_tokenTeste, autenticacao.Token);
            Assert.Equal(EMAIL_USUARIO_TESTE, autenticacao.Email);
            Assert.True(autenticacao.UsuarioCadastrado);
            Assert.False(_mensagens.PossuiErros);
        }

        private void instanciarAutenticacaoLoginSocialServico()
        {
            var servicoTokenMock = new Mock<IServicoToken>();
            servicoTokenMock.Setup(service =>
                service.GerarToken(It.IsAny<IEnumerable<System.Security.Claims.Claim>>(), It.IsAny<string>(), It.IsAny<TimeSpan>()))
                    .Returns(_tokenTeste);

            var redesSociaisConfiguracao = new RedesSociaisConfiguracao { Google = new GoogleConfiguracao() };

            _autenticacaoLoginSocialServico = new AutenticacaoLoginSocialServico(servicoTokenMock.Object, new ConfiguracaoAutenticacao(), _mensagens,
                _loggerMock, _provedorFacebookMock.Object, _contexto, redesSociaisConfiguracao, _provedorAppleMock.Object);
        }

        private async Task criarDadosUsuarioLoginSocial(RedeSocial redeSocial)
        {
            var usuarioDominio = await CriarUsuarioTesteAsync();
            usuarioDominio.AdicionarVinculoRedeSocial(redeSocial, EMAIL_USUARIO_TESTE);
            await _contexto.SaveChangesAsync();
        }

    }
}
