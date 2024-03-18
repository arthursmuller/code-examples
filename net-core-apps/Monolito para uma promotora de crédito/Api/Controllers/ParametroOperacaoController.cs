using Aplicacao;
using Aplicacao.Model.ParametroOperacao;
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
    [ExcludeFromCodeCoverage]
    public class ParametroOperacaoController : BaseController
    {
        private readonly ParametroOperacaoServico _parametroOperacaoServico;

        public ParametroOperacaoController(ParametroOperacaoServico parametroOperacaoServico, IBemMensagens mensagens) : base(mensagens)
            => _parametroOperacaoServico = parametroOperacaoServico;

        [HttpGet("parametros-operacao")]
        public async Task<RetornoApi<IEnumerable<ParametroOperacaoModel>>> Get()
        {
            var parametrosOperacao = await _parametroOperacaoServico.BuscarParametrosOperacao();

            return FormatarRetorno(parametrosOperacao);
        }

        [HttpPost("parametros-operacao")]
        [Authorize(Roles = "admin")]
        public async Task<RetornoApi<ParametroOperacaoNovoModel>> Post([FromBody] ParametroOperacaoCriacaoModel parametroOperacao)
        {
            var parametroOperacaoNovo = await _parametroOperacaoServico.CriarParametroOperacao(parametroOperacao);

            return FormatarRetorno(parametroOperacaoNovo);
        }

        [HttpPut("parametros-operacao/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<RetornoApi<bool>> Put([FromBody] ParametroOperacaoAtualizacaoModel parametroOperacao, int id)
        {
            var parametroOperacaoAtualizado = await _parametroOperacaoServico.AtualizarParametroOperacao(parametroOperacao, id);

            return FormatarRetorno(parametroOperacaoAtualizado);
        }

        [HttpDelete("parametros-operacao/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<RetornoApi<bool>> Delete(int id)
        {
            var parametroOperacaoDeletado = await _parametroOperacaoServico.DeletarParametroOperacao(id);

            return FormatarRetorno(parametroOperacaoDeletado);
        }
    }
}
