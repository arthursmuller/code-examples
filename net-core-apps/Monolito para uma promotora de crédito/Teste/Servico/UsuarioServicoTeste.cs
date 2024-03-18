using Aplicacao;
using Aplicacao.Model.Autenticacao;
using Aplicacao.Servico;
using B.Configuracao;
using Dominio;
using Dominio.Enum.TemplateEmail;
using Infraestrutura;
using Infraestrutura.Fila.Email;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class UsuarioServicoTeste : ServicoTesteBase
    {
        private readonly ITermoServico _termoServico;
        private readonly UsuarioServico _usuarioServico;
        private readonly AesCryptography _aes;
        private readonly UsuarioDominio _usuarioTeste;

        public UsuarioServicoTeste() : base()
        {
            _aes = new AesCryptography("aesUnitTestKey01", "aesUnitTestKeyIV");

            var servicoAutenticacaoBaseMock = new Mock<IAutenticacaoLoginSocialServico>();
            var servicoEmailMock = new Mock<IEmailServico>();
            var producerEmailMock = new Mock<IProducerEmail>();
            var termoServicoMock = new Mock<ITermoServico>();

            _usuarioTeste = CriarUsuarioTeste();

            var usuarioLogado = new UsuarioLoginDominio()
            {
                IdUsuario = _usuarioTeste.ID,
            };

            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("ambiente-frontend-url", "teste"),
            }, null, null);

            servicoAutenticacaoBaseMock.Setup(service =>
                service.GerarToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(new AutenticacaoModel());

            servicoEmailMock.Setup(service =>
                service.RegistrarEmail(It.IsAny<TemplateEmailFinalidade>(), It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<int>()))
                    .ReturnsAsync(true);

            termoServicoMock.Setup(service =>
                service.ObterTermosPendentesAceiteUsuario(usuarioLogado.IdUsuario));

            _termoServico = new TermoServico(_mensagens, It.IsAny<IUsuarioLogin>(), _contexto);

            _usuarioServico = new UsuarioServico(_mensagens, usuarioLogado, _contexto, _aes, servicoAutenticacaoBaseMock.Object, servicoEmailMock.Object, configuracao, _termoServico);
        }

        [Fact]
        public async Task RequisitarTrocaSenha_QuandoEmailValido_DeveRetornarSucesso()
        {
            var resultado = await _usuarioServico.RequisitarTrocaDeSenha(_usuarioTeste.Email);

            var requisicoes = await _contexto.UsuarioRecuperacaoSenhas
                .Where(u => u.IdUsuario == _usuarioTeste.ID)
                .ToListAsync();

            Assert.Single(requisicoes);
            Assert.False(requisicoes.First().Expirado);
            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
        }

        [Fact]
        public async Task RequisitarTrocaSenha_QuandoEmailInvalido_DeveRetornarErro()
        {
            var resultado = await _usuarioServico.RequisitarTrocaDeSenha("wrong@email.com");

            var requisicoes = await _contexto.UsuarioRecuperacaoSenhas
                .Where(u => u.IdUsuario == _usuarioTeste.ID)
                .ToListAsync();

            Assert.Empty(requisicoes);
            Assert.True(_mensagens.PossuiErros);
            Assert.False(resultado);
        }

        [Fact]
        public async Task RequisitarTrocaSenha_QuandoEmailNaoConfirmado_DeveRetornarErro()
        {
            var resultado = await _usuarioServico.RequisitarTrocaDeSenha("wrong@email.com");

            var requisicoes = await _contexto.UsuarioRecuperacaoSenhas
                .Where(u => u.IdUsuario == _usuarioTeste.ID)
                .ToListAsync();

            Assert.Empty(requisicoes);
            Assert.True(_mensagens.PossuiErros);
            Assert.False(resultado);
        }

        [Fact]
        public async Task RequisitarTrocaSenha_QuandoJaExisteRequesicao_DeveInvalidarAPendente()
        {
            await _usuarioServico.RequisitarTrocaDeSenha(_usuarioTeste.Email);
            var resultado = await _usuarioServico.RequisitarTrocaDeSenha(_usuarioTeste.Email);

            var requisicoes = await _contexto.UsuarioRecuperacaoSenhas
                .Where(u => u.IdUsuario == _usuarioTeste.ID)
                .ToListAsync();

            Assert.Single(requisicoes.Where(r => r.Expirado));
            Assert.Single(requisicoes.Where(r => !r.Expirado));
            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
        }

        [Fact]
        public async Task ConsultarToken_QuandoValido_DeveRetornarSucesso()
        {
            await _usuarioServico.RequisitarTrocaDeSenha(_usuarioTeste.Email);

            var requisicao = await _contexto.UsuarioRecuperacaoSenhas
                .FirstOrDefaultAsync(u => u.IdUsuario == _usuarioTeste.ID);

            var resultado = await _usuarioServico
                .ConsultarValidadeTokenSenha(_aes.Decrypt(requisicao.Token));

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado);
        }

        [Fact]
        public async Task ConsultarToken_Email_QuandoValido_DeveRetornarSucesso()
        {
            await _usuarioServico.EnviarEmailConfirmacaoEmail(_usuarioTeste);

            await _contexto.UsuarioConfirmacaoEmailDominio.AddAsync(new UsuarioConfirmacaoEmailDominio(_usuarioTeste.ID, ""));

            var requisicao = await _contexto.UsuarioConfirmacaoEmailDominio
                .FirstOrDefaultAsync(u => u.IdUsuario == _usuarioTeste.ID);

            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task ConsultarToken_QuandoInvalido_DeveRetornarErro()
        {
            var resultado = await _usuarioServico
                .ConsultarValidadeTokenSenha("doYkl3PfT8B7LNHYhVKFNqRvFFpXAgIU8RxO7GumNz4=");

            Assert.True(_mensagens.PossuiErros);
            Assert.True(_mensagens.TemErroFormulario);
            Assert.False(resultado);
        }

        [Fact]
        public async Task ConsultarToken_QuandoExpirado_DeveRetornarErro()
        {
            await _usuarioServico.RequisitarTrocaDeSenha(_usuarioTeste.Email);
            var requisicao = await _contexto.UsuarioRecuperacaoSenhas
                .FirstOrDefaultAsync(u => u.IdUsuario == _usuarioTeste.ID);

            requisicao.Invalidar();

            await _contexto.SaveChangesAsync();

            var resultado = await _usuarioServico.ConsultarValidadeTokenSenha(_aes.Decrypt(requisicao.Token));

            Assert.True(_mensagens.PossuiErros);
            Assert.False(_mensagens.TemErroFormulario);
            Assert.False(resultado);
        }

        [Fact]
        public async Task TrocarSenha_QuandoTokenCorreto_DeveAtualizarUsuarioEExpirarRequisicao()
        {
            var senhaOriginal = _usuarioTeste.Senha;

            await _usuarioServico.RequisitarTrocaDeSenha(_usuarioTeste.Email);
            var requisicao = await _contexto.UsuarioRecuperacaoSenhas
                .FirstOrDefaultAsync(u => u.IdUsuario == _usuarioTeste.ID);
            var resultado = await _usuarioServico.AtualizarSenhaUsuarioELogar(_aes.Decrypt(requisicao.Token), "NewPassword01");

            var requisicaoAtualizada = await _contexto.UsuarioRecuperacaoSenhas
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(u => u.IdUsuario == _usuarioTeste.ID);

            Assert.NotEqual(requisicaoAtualizada.Usuario.Senha, senhaOriginal);
            Assert.True(requisicaoAtualizada.Expirado);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task TrocarSenha_QuandoTokenIncorreto_DeveRetornarErro()
        {
            await _usuarioServico.RequisitarTrocaDeSenha(_usuarioTeste.Email);
            var resultado = await _usuarioServico.AtualizarSenhaUsuarioELogar("doYkl3PfT8B7LNHYhVKFNqRvFFpXAgIU8RxO7GumNz4=", "NewPassword01");

            Assert.True(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task CriacaoUsuario_QuandoCPFJaExiste_DeveRetornarErro()
        {
            var usuarioExistente = await CriarUsuarioTesteAsync();

            UsuarioCriacaoModel novoUsuario = new UsuarioCriacaoModel()
            {
                Nome = "Unit, the Test Junior",
                Email = "new_email@test.com",
                Senha = "1234ABcd",
                CPF = usuarioExistente.CPF,
            };

            var resultado = await _usuarioServico.CriarUsuario(novoUsuario);

            Assert.True(_mensagens.PossuiErros);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task CriacaoUsuario_QuandoEmailJaExiste_DeveRetornarErro()
        {
            var usuarioExistente = await CriarUsuarioTesteAsync();

            UsuarioCriacaoModel novoUsuario = new UsuarioCriacaoModel()
            {
                Nome = "Unit, the Test Junior",
                Email = usuarioExistente.Email,
                Senha = "1234ABcd",
                CPF = "99988877766",
            };

            var resultado = await _usuarioServico.CriarUsuario(novoUsuario);

            Assert.True(_mensagens.PossuiErros);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task CriacaoUsuario_QuandoInformarNovos_DeveCadastrar()
        {
            UsuarioCriacaoModel novoUsuario = new UsuarioCriacaoModel()
            {
                Nome = "Unit, the Test Junior",
                Email = "new@test.com",
                Senha = "1234ABcd",
                CPF = "45396865008",
            };

            var resultado = await _usuarioServico.CriarUsuario(novoUsuario);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
            Assert.NotEqual(0, resultado.Id);
        }

        [Fact]
        public async Task AtualizarSenhaUsuarioLogado_QuandoEncontrado_DeveAtualizarERetornarAutenticado()
        {
            var usuarioTesteContexto = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.ID == _usuarioTeste.ID);
            usuarioTesteContexto.SetSenha(_aes.Encrypt(usuarioTesteContexto.Senha));
            await _contexto.SaveChangesAsync();

            var requisicao = new UsuarioAtualizacaoSenhaModel
            {
                NovaSenha = "4321dcBA",
                SenhaAtual = "1234ABcd",
            };

            var resultado = await _usuarioServico.AtualizarSenhaUsuarioLogado(requisicao);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultado);
        }

        [Fact]
        public async Task AtualizarSenhaUsuarioLogado_QuandoSenhaAtualIncorreta_DeveRetornarErro()
        {
            var requisicao = new UsuarioAtualizacaoSenhaModel
            {
                NovaSenha = "wrong-pw",
                SenhaAtual = "1234ABcd",
            };

            var resultado = await _usuarioServico.AtualizarSenhaUsuarioLogado(requisicao);

            Assert.True(_mensagens.PossuiErros);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task CriacaoUsuario_QuandoExistirTermoPendenteDeAceite_DeveCadastrarAceite()
        {
            await criarTermos();

            UsuarioCriacaoModel novoUsuario = new UsuarioCriacaoModel()
            {
                Nome = "Unit, the Test Junior",
                Email = "new@test.com",
                Senha = "1234ABcd",
                CPF = "45396865008",
            };

            var resultadoUsuario = await _usuarioServico.CriarUsuario(novoUsuario);

            var resultadoUsuarioTermo = await buscarUsuarioTermo(resultadoUsuario);

            var termosPendenteDeAceite = await _termoServico.ObterTermosPendentesAceiteUsuario(resultadoUsuario.Id);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(resultadoUsuario);
            Assert.NotEqual(0, resultadoUsuario.Id);
            Assert.Equal(2, resultadoUsuarioTermo.Count);
            Assert.Empty(termosPendenteDeAceite);
        }

        private async System.Threading.Tasks.Task<List<UsuarioTermoDominio>> buscarUsuarioTermo(UsuarioModel resultadoUsuario)
        {
            return await _contexto.UsuariosTermos
                                                .Where(termo => termo.IdUsuario.Equals(resultadoUsuario.Id))
                                                .AsNoTracking()
                                                .ToListAsync();
        }

        private async Task criarTermos()
        {
            var tiposTermo = new TipoTermoDominio[] {
                            new TipoTermoDominio(Dominio.Enum.TipoTermo.CriacaoUsuario, "Criação de Usuário"),
                            new TipoTermoDominio(Dominio.Enum.TipoTermo.ImportacaoDados, "Aceite de Importação de Dados"),
                        };

            _contexto.TiposTermo.AddRange(tiposTermo);
            await _contexto.SaveChangesAsync();

            var termos = new TermoDominio[] {
                            new TermoDominio(Dominio.Enum.TipoTermo.CriacaoUsuario, "Criação de Usuário", null, DateTime.Now),
                            new TermoDominio(Dominio.Enum.TipoTermo.ImportacaoDados, "Aceite de Importação de Dados", null, DateTime.Now)
                        };

            _contexto.Termos.AddRange(termos);
            await _contexto.SaveChangesAsync();
        }

        [Fact]
        public async Task AtualizarUsuario_Deve_Mudar_Propriedades()
        {
            var usuarioAtualizacaoModel = new UsuarioAtualizacaoModel
            {
                Email = "teste@teste.com",
                CPF = "02981603078",
                Nome = "teste"
            };

            await _usuarioServico.AtualizarUsuario(usuarioAtualizacaoModel);

            Assert.Equal(_usuarioTeste.Email, usuarioAtualizacaoModel.Email);
            Assert.Equal(_usuarioTeste.CPF, usuarioAtualizacaoModel.CPF);
            Assert.Equal(_usuarioTeste.Nome, usuarioAtualizacaoModel.Nome);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task AtualizarUsuario_Deve_Retornar_Null_Quando_Nao_Encontra_UsuarioLogado()
        {
            var cliente = await _contexto.Clientes.FirstOrDefaultAsync(c => c.ID == _usuarioTeste.Cliente.ID);
            _contexto.Clientes.Remove(cliente);
            _contexto.Usuarios.Remove(_usuarioTeste);
            await _contexto.SaveChangesAsync();
            var usuarioAtualizacaoModel = new UsuarioAtualizacaoModel
            {
                Email = "teste@teste.com",
                CPF = "02981603078",
                Nome = "teste"
            };
            
            var resposta = await _usuarioServico.AtualizarUsuario(usuarioAtualizacaoModel);

            Assert.Null(resposta.CPF);
            Assert.Null(resposta.Cliente);
            Assert.Null(resposta.Email);
            Assert.Null(resposta.Loja);
            Assert.Null(resposta.Nome);
            Assert.False(resposta.Administrador);
            Assert.Equal(resposta.Id, 0);
            Assert.True(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task TrocarSenha_Senha_Deve_Retornar_False_Quando_LoginNaoPermitido()
        {
            var usuarioTeste = await CriarUsuarioTesteEmailNaoConfirmadoAsync();
            var resposta = await _usuarioServico.RequisitarTrocaDeSenha(usuarioTeste.Email);

            Assert.False(resposta);
            Assert.True(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task ConfirmarEmail_Deve_Retornar_Token()
        {
            var usuarioTeste = await CriarUsuarioTesteEmailNaoConfirmadoAsync();

            await _usuarioServico.EnviarEmailConfirmacaoEmail(usuarioTeste);

            var requisicao = await _contexto.UsuarioConfirmacaoEmailDominio
                .FirstOrDefaultAsync(u => u.IdUsuario == usuarioTeste.ID);

            var resposta = await _usuarioServico.ConfirmarEmailELogar(requisicao.Token, usuarioTeste.ID);
            Assert.False(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task ConfirmarEmail_Deve_Possuir_Erros()
        {
            var usuarioTeste = await CriarUsuarioTesteEmailNaoConfirmadoAsync();

            await _usuarioServico.EnviarEmailConfirmacaoEmail(usuarioTeste);

            var requisicao = await _contexto.UsuarioConfirmacaoEmailDominio
                .FirstOrDefaultAsync(u => u.IdUsuario == usuarioTeste.ID);

            requisicao.Invalidar();

            var resposta = await _usuarioServico.ConfirmarEmailELogar(requisicao.Token, usuarioTeste.ID);

            Assert.True(_mensagens.PossuiErros);
        }
    }
}
