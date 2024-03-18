using Aplicacao.Model.Autenticacao;
using Aplicacao.Servico;
using B.Autenticacao;
using Dominio;
using Infraestrutura;
using Infraestrutura.Autenticacao;
using Moq;
using SharedKernel.ValueObjects.v2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class AutenticacaoServicoTeste: ServicoTesteBase
    {
        private readonly AutenticacaoServico _autenticacaoServico;
        private readonly AesCryptography _aes;
        private readonly string _tokenTeste = "FakeToken";


        public AutenticacaoServicoTeste(): base()
        {
            _aes = new AesCryptography("aesUnitTestKey01", "aesUnitTestKeyIV");

            ConfiguracaoAutenticacao configuracaoAutenticacao = new ConfiguracaoAutenticacao
            {
                ChaveJwt = "test",
                SegundosParaAtualizarToken = 600000,
                SegundosParaExpirarToken = 600000,
            };

            var servicoTokenMock = new Mock<IServicoToken>();

            servicoTokenMock.Setup(service => 
                service.GerarToken(It.IsAny<IEnumerable<System.Security.Claims.Claim>>(), It.IsAny<string>(), It.IsAny<TimeSpan>()))
                    .Returns(_tokenTeste);

            _autenticacaoServico = new AutenticacaoServico(configuracaoAutenticacao, _aes, _mensagens, _contexto, servicoTokenMock.Object);
        }

        [Fact]
        public async Task Autenticar_ComCPFValido_DeveRetornarToken()
        {
            var (usuarioTeste, senha) = await criarUsuarioTesteComCriptografia();
            
            LoginModel requisicao = new LoginModel()
            {
                Cpf = usuarioTeste.CPF,
                Senha = senha,
            };
            
            var resultado = await _autenticacaoServico.Autenticar(requisicao);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(resultado.Nome, usuarioTeste.Nome);
            Assert.Equal(resultado.Token, _tokenTeste);
        }

        [Fact]
        public async Task Autenticar_ComEmailValido_DeveRetornarToken()
        {
            var (usuarioTeste, senha) = await criarUsuarioTesteComCriptografia();
            
            LoginModel requisicao = new LoginModel()
            {
                Email = usuarioTeste.Email,
                Senha = senha,
            };
            
            var resultado = await _autenticacaoServico.Autenticar(requisicao);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.Equal(resultado.Nome, usuarioTeste.Nome);
            Assert.Equal(resultado.Token, _tokenTeste);
        }

        [Fact]
        public async Task Autenticar_ComSenhaInvalida_DeveRetornarErro()
        {
            var (usuarioTeste, _) = await criarUsuarioTesteComCriptografia();
            
            LoginModel requisicao = new LoginModel()
            {
                Cpf = usuarioTeste.Email,
                Senha = "senha_invalida",
            };
            
            var resultado = await _autenticacaoServico.Autenticar(requisicao);

            Assert.True(_mensagens.PossuiErros);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task Autenticar_ComUsuarioInvalido_DeveRetornarErro()
        {
            var (usuarioTeste, senha) = await criarUsuarioTesteComCriptografia();
            
            LoginModel requisicao = new LoginModel()
            {
                Email = "email_invalido",
                Senha = senha,
            };
            
            var resultado = await _autenticacaoServico.Autenticar(requisicao);

            Assert.True(_mensagens.PossuiErros);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task Autenticar_Com_Email_Nao_Confirmado_Deve_Retornar_Null()
        {
            var usuario= await CriarUsuarioTesteEmailNaoConfirmadoAsync();

            LoginModel requisicao = new LoginModel()
            {
                Email = usuario.Email,
                Senha = usuario.Senha,
            };

            var resultado = await _autenticacaoServico.Autenticar(requisicao);

            Assert.True(_mensagens.PossuiErros);
            Assert.Null(resultado);
        }

        private async Task<(UsuarioDominio, string)> criarUsuarioTesteComCriptografia()
        {
            var senha = "1234ABcd";
            var senhaCriptografada = _aes.Encrypt(senha);

            var nome = "Uni, the Test";
            var usuario = new UsuarioDominio(nome, "unithe@test.com", true, new CPF(CPF_USUARIO_TESTE), senhaCriptografada, new ClienteDominio(nome));

            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();

            return (usuario, senha);
        }
    }
}
