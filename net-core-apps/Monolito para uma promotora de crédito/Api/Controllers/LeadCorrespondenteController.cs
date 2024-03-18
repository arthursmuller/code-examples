using Aplicacao;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("leads-correspondente")]
    [ExcludeFromCodeCoverage]
    public class LeadCorrespondenteController : BaseController
    {
        private readonly LeadCorrespondenteServico _leadCorrespondenteServico;

        public LeadCorrespondenteController(LeadCorrespondenteServico leadCorrespondenteServico, IBemMensagens mensagens) : base(mensagens)
            => _leadCorrespondenteServico = leadCorrespondenteServico;

        [HttpPost()]
        public async Task<RetornoApi<int?>> Post([FromBody] LeadCorrespondenteCriacaoModel lead)
        {
            var leadNova = await _leadCorrespondenteServico.GravarLead(lead);

            return FormatarRetorno(leadNova);
        }
    }
}