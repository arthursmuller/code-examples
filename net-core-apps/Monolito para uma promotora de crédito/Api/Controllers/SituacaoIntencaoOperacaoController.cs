using Aplicacao.Model.SituacaoIntencaoOperacao;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("situacoes-intencao-operacao")]
    [ExcludeFromCodeCoverage]
    public class SituacaoIntencaoOperacaoController : BaseController
    {
        private readonly SituacaoIntencaoOperacaoServico _situacaoIntencaoOperacaoServico;

        public SituacaoIntencaoOperacaoController(SituacaoIntencaoOperacaoServico situacaoIntencaoOperacaoServico, IBemMensagens mensagens) : base(mensagens)
            => _situacaoIntencaoOperacaoServico = situacaoIntencaoOperacaoServico;

        [HttpGet]
        public async Task<RetornoApi<IEnumerable<SituacaoIntencaoOperacaoModel>>> Get()
        {
            var situacoes = await _situacaoIntencaoOperacaoServico.ListarSituacoesIntencaoOperacao();

            return FormatarRetorno(situacoes);
        }

        [HttpGet("extraordinarias")]
        public async Task<RetornoApi<IEnumerable<SituacaoIntencaoOperacaoModel>>> ListarSituacoesIntencaoOperacaoExtraordinarias()
        {
            var situacoes = await _situacaoIntencaoOperacaoServico.ListarSituacoesIntencaoOperacao(true);

            return FormatarRetorno(situacoes);
        }
    }
}
