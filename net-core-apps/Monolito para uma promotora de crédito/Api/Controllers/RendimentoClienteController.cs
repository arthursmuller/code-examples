using Aplicacao.Model.RendimentoCliente;
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
    [Route("clientes/autenticado")]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class RendimentoClienteController : BaseController
    {
        private readonly RendimentoClienteServico _rendimentoServico;

        public RendimentoClienteController(IBemMensagens mensagens, RendimentoClienteServico rendimentoClienteServico) : base(mensagens)
            => _rendimentoServico = rendimentoClienteServico;

        [HttpGet("rendimentos")]
        public async Task<RetornoApi<IEnumerable<RendimentoClienteExibicaoModel>>> Get()
        {
            var rendimentos = await _rendimentoServico.BuscarRendimentosPorCliente();

            return FormatarRetorno(rendimentos);
        }

        [HttpPost("rendimentos")]
        public async Task<RetornoApi<RendimentoClienteExibicaoModel>> Post([FromBody] RendimentoClienteModel rendimento)
        {
            var rendimentoNovo = await _rendimentoServico.GravarRendimento(rendimento);

            return FormatarRetorno(rendimentoNovo);
        }

        [HttpPut("rendimentos/{idRendimento}")]
        public async Task<RetornoApi<RendimentoClienteExibicaoModel>> Put(int idRendimento, [FromBody] RendimentoClienteModel rendimento)
        {
            var rendimentoNovo = await _rendimentoServico.AtualizarRendimento(idRendimento, rendimento);

            return FormatarRetorno(rendimentoNovo);
        }

        [HttpDelete("rendimentos/{idRendimento}")]
        public async Task<RetornoApi<bool>> Delete(int idRendimento)
        {
            var rendimentoNovo = await _rendimentoServico.RemoverRendimento(idRendimento);

            return FormatarRetorno(rendimentoNovo);
        }

        [HttpGet("rendimentos-siape/margens")]
        public async Task<RetornoApi<RendimentoSiapeConsultaMargemModel>> ConsultarMargemSiape([FromQuery] int? idRendimento)
        {
            var rendimentos = await _rendimentoServico.ConsultarMargem(idRendimento);

            return FormatarRetorno(rendimentos);
        }
    }
}
