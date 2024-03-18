using Aplicacao.Model.TipoOperacao;
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
    [ExcludeFromCodeCoverage]
    public class TipoOperacaoController : BaseController
    {
        private readonly TipoOperacaoServico _tipoOperacaoServico;

        public TipoOperacaoController(TipoOperacaoServico tipoOperacaoServico, IBemMensagens mensagens) : base(mensagens)
            => _tipoOperacaoServico = tipoOperacaoServico;

        [HttpGet("tipos-operacao/{id}")]
        public async Task<RetornoApi<TipoOperacaoModel>> Get(int id)
        {
            var tipoOperacao = await _tipoOperacaoServico.BuscarTipoOperacao(id);

            return FormatarRetorno(tipoOperacao);
        }

        [HttpGet("tipos-operacao")]
        public async Task<RetornoApi<IEnumerable<TipoOperacaoModel>>> Get()
        {
            var tiposOperacao = await _tipoOperacaoServico.ListarTiposOperacao();

            return FormatarRetorno(tiposOperacao);
        }
    }
}
