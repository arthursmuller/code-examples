using Aplicacao.Servico;
using Dominio;
using Dominio.Enum.Notificacoes;
using Dominio.Resource;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Teste.Servico
{
    public class NotificacaoServicoTeste : ServicoTesteBase
    {
        private readonly NotificacaoServico _notificacaoServico;
        private UsuarioDominio _usuarioTeste;

        public NotificacaoServicoTeste() : base()
        {
            _usuarioTeste = CriarUsuarioTeste();

            UsuarioLoginDominio usuarioLogin = new UsuarioLoginDominio { IdUsuario = _usuarioTeste.ID, Nome = _usuarioTeste.Nome };

            var templateBuilder = new TemplateBuilderServico(_mensagens);
            _notificacaoServico = new NotificacaoServico(_mensagens, usuarioLogin, _contexto, templateBuilder);
        }

        [Fact]
        public async Task CriarNotificacao_QuandoTemplateNaoEncontrado_DeveRetornarErro()
        {
            NotificacaoDominio resultado = await _notificacaoServico.GerarNotificacao(_usuarioTeste.ID, NotificacaoFinalidade.RefCadastro, null);

            Assert.Null(resultado);
            Assert.Contains(_mensagens.BuscarAlertas(), alertas => alertas.Mensagem.Equals(string.Format(Mensagens.TemplateNotificacao_Ausente, NotificacaoFinalidade.RefCadastro.ToString())));
        }

        [Fact]
        public async Task GerarNotificacao_QuandoEncontradoTemplate_DeveGerarNotificacao()
        {
            await criarTemplate();

            var chavesLayout = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["test"] = "unit",
                ["test2"] = "unit2",
            };

            NotificacaoDominio resultado = await _notificacaoServico.GerarNotificacao(_usuarioTeste.ID, NotificacaoFinalidade.RefCadastro, chavesLayout);

            Assert.False(_mensagens.PossuiErros);
            Assert.True(resultado != null);
            Assert.DoesNotContain("[[", resultado.Titulo);
            Assert.DoesNotContain("[[", resultado.UrlReferencia);
            Assert.DoesNotContain("[[", resultado.Descricao);
            Assert.Contains("unit2 unit", resultado.Descricao);
            Assert.NotNull(await _contexto.Notificacoes.Where(n => n.IdUsuario == _usuarioTeste.ID).ToListAsync());
        }

        [Fact]
        public async Task MarcarVisualizacao_QuandoEncontrada_DevePersistir()
        {
            var notificacao = await criarNotificacao();

            Assert.Null(notificacao.DataVisualizacao);

            await _notificacaoServico.MarcarVisualizacao(notificacao.ID);

            Assert.False(_mensagens.PossuiErros);
            Assert.NotNull(notificacao.DataVisualizacao);
        }

        [Fact]
        public async Task CompletarNotificacao_QuandoNotificacaoNaoEncontrada_DeveRetornarNullComAlerta()
        {
            var idNotificacao = 0;
            var notificacao = await _notificacaoServico.CompletarNotificacao(idNotificacao);

            Assert.Null(notificacao);
            Assert.Contains(_mensagens.BuscarAlertas(), alerta => alerta.Mensagem.Equals(string.Format(Mensagens.Notificacao_NaoEncontrada, idNotificacao)));
        }

        [Fact]
        public async Task CompletarNotificacao_QuandoDadosValidos_DeveRegistrarSemErros()
        {
            var notificacao = await criarNotificacao();

            var completudeNotificacao = await _notificacaoServico.CompletarNotificacao(notificacao.ID);
            var notificacaoAtualizada = await _contexto.Notificacoes.AsNoTracking().FirstOrDefaultAsync(n => n.ID.Equals(notificacao.ID));

            Assert.NotNull(completudeNotificacao);
            Assert.NotNull(notificacaoAtualizada);
            Assert.True(notificacaoAtualizada.Completo);
        }

        private async Task criarTemplate()
        {

            NotificacaoTemplateDominio templateTeste = new NotificacaoTemplateDominio(
                "[[test]]",
                "[[test2]] [[test]]",
                "[[test]]",
                NotificacaoSeveridade.Media,
                NotificacaoFinalidade.RefCadastro
            );

            await _contexto.AddAsync(templateTeste);

            await _contexto.SaveChangesAsync();
        }

        private async Task<NotificacaoDominio> criarNotificacao()
        {
            var notificacao = new NotificacaoDominio(_usuarioTeste.ID, null, "", "", "", NotificacaoSeveridade.Baixa, NotificacaoFinalidade.RefCadastro);
            await _contexto.AddAsync(notificacao);
            await _contexto.SaveChangesAsync();

            return notificacao;
        }
    }
}
