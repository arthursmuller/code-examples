using Aplicacao.Model.Notificacoes;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("clientes/autenticado/notificacoes")]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class NotificacaoController : BaseController
    {
        private readonly INotificacaoServico _notificacaoServico;

        public NotificacaoController(INotificacaoServico notificacaoServico, IBemMensagens mensagens) : base(mensagens)
        {
            _notificacaoServico = notificacaoServico;
        }

        [HttpGet]
        public async Task<RetornoApi<IEnumerable<NotificacaoModel>>> Get()
        {
            var notificacoes = await _notificacaoServico.BuscarNotificacoesAutenticado();

            return FormatarRetorno(notificacoes);
        }

        [HttpPatch("{idNotificacao}/marcar-visualizacao")]
        public async Task<RetornoApi<NotificacaoModel>> MarcarVisualizacao([FromRoute] int idNotificacao)
        {
            var notificacao = await _notificacaoServico.MarcarVisualizacao(idNotificacao);

            return FormatarRetorno(notificacao);
        }
    }
}
