using Aplicacao.Model.AcessoDadosPessoais;
using Aplicacao.Servico;
using B.Configuracao;
using Dominio;
using Dominio.Enum.TemplateEmail;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class AcessoDadosPessoaisServicoTeste : ServicoTesteBase
    {
        private IEmailServico _emailServico = new Mock<IEmailServico>().Object;

        [Fact]
        public async Task CriarSolicitacaoDeAcesso_ComTelefoneInvalido_DeveRetornarNullComMensagemDeErro()
        {
            var novaSolicitacao = new SolicitacaoAcessoDadosPessoaisEnvioModel
            {
                Nome = "Carabina",
                Sobrenome = "Tiro Certo",
                DataNascimento = DateTime.Now.AddYears(-60),
                NomeMae = "Pólvora Seca",
                Email = "carabina@tirocerto.com",
                TelefoneCompleto = new SolicitacaoAcessoDadosPessoaisTelefoneEnvioModel()
            };

            var acessoDadosPessoaisServico = obterInstanciaAcessoDadosPessoaisServico();
            var solicitacao = await acessoDadosPessoaisServico.CriarSolicitacaoDeAcesso(novaSolicitacao);

            Assert.Null(solicitacao);
            Assert.True(_mensagens.PossuiErros);
        }

        [Fact]
        public async Task CriarSolicitacaoDeAcesso_NotificacaoNaoEnviada_DeveRetornarComNotificacaoEnviadaFalseSemErros()
        {
            InstanciarAdapter();

            var novaSolicitacao = new SolicitacaoAcessoDadosPessoaisEnvioModel
            {
                Nome = "Carabina",
                Sobrenome = "Tiro Certo",
                DataNascimento = DateTime.Now.AddYears(-60),
                NomeMae = "Pólvora Seca",
                Email = "carabina@tirocerto.com",
                TelefoneCompleto = new SolicitacaoAcessoDadosPessoaisTelefoneEnvioModel
                {
                    DDD = "51",
                    Telefone = "96965-4215"
                },
                Motivo = "Teste"
            };

            var emailServicoMock = new Mock<IEmailServico>();
            emailServicoMock
                .Setup(s => s.RegistrarEmail(It.IsAny<TemplateEmailFinalidade>(), It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<int?>()))
                .ReturnsAsync(false);
            _emailServico = emailServicoMock.Object;

            var acessoDadosPessoaisServico = obterInstanciaAcessoDadosPessoaisServico();
            var solicitacao = await acessoDadosPessoaisServico.CriarSolicitacaoDeAcesso(novaSolicitacao);

            Assert.NotNull(solicitacao);
            Assert.False(_mensagens.PossuiErros);

            var solicitacaoGravada = await obterSolicitacaoGravada(solicitacao.Id);

            Assert.NotNull(solicitacaoGravada);
            Assert.False(solicitacaoGravada.NotificacaoEnviada);
        }

        [Fact]
        public async Task CriarSolicitacaoDeAcesso_NotificacaoEnviada_DeveRetornarComNotificacaoEnviadaTrueSemErros()
        {
            InstanciarAdapter();

            var novaSolicitacao = obterNovaSolicitacaoValida();

            var emailServicoMock = new Mock<IEmailServico>();
            emailServicoMock
                .Setup(s => s.RegistrarEmail(It.IsAny<TemplateEmailFinalidade>(), It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<Dictionary<string, object>>(), It.IsAny<int?>()))
                .ReturnsAsync(true);
            _emailServico = emailServicoMock.Object;

            var acessoDadosPessoaisServico = obterInstanciaAcessoDadosPessoaisServico();
            var solicitacao = await acessoDadosPessoaisServico.CriarSolicitacaoDeAcesso(novaSolicitacao);

            Assert.NotNull(solicitacao);
            Assert.False(_mensagens.PossuiErros);

            var solicitacaoGravada = await obterSolicitacaoGravada(solicitacao.Id);

            Assert.NotNull(solicitacaoGravada);
            Assert.True(solicitacaoGravada.NotificacaoEnviada);
        }

        private SolicitacaoAcessoDadosPessoaisEnvioModel obterNovaSolicitacaoValida()
        {
            return new SolicitacaoAcessoDadosPessoaisEnvioModel
            {
                Nome = "Carabina",
                Sobrenome = "Tiro Certo",
                DataNascimento = DateTime.Now.AddYears(-60),
                NomeMae = "Pólvora Seca",
                Email = "carabina@tirocerto.com",
                TelefoneCompleto = new SolicitacaoAcessoDadosPessoaisTelefoneEnvioModel
                {
                    DDD = "51",
                    Telefone = "96965-4215"
                },
                Motivo = "Teste"
            };
        }

        private AcessoDadosPessoaisServico obterInstanciaAcessoDadosPessoaisServico()
        {
            var configuracao = new Configuracao(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("email_solicitacao_dados_pessoais", "teste@teste.com"),
            }, null, null);

            return new AcessoDadosPessoaisServico(_mensagens, _contexto, _emailServico, configuracao);
        }

        private async Task<SolicitacaoAcessoDadosPessoaisClienteDominio> obterSolicitacaoGravada(int idSolicitacao)
        {
            return await _contexto.SolicitacoesAcessoDadosPessoaisClientes
                            .AsNoTracking()
                            .FirstOrDefaultAsync(s => s.ID.Equals(idSolicitacao));
        }
    }
}
