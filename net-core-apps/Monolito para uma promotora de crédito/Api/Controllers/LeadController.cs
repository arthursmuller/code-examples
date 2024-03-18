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
    [ExcludeFromCodeCoverage]
    public class LeadController : BaseController
    {
        private readonly LeadServico _leadServico;

        public LeadController(LeadServico leadServico, IBemMensagens mensagens) : base(mensagens)
            => _leadServico = leadServico;

        [HttpGet("leads/{id}")]
        public async Task<RetornoApi<LeadModel>> Get(int id)
        {
            var lead = await _leadServico.BuscarLead(id);

            return FormatarRetorno(lead);
        }

        [HttpPost("leads")]
        public async Task<RetornoApi<LeadNovaModel>> Post([FromBody] LeadCriacaoModel lead)
        {
            var leadNova = await _leadServico.GravarLead(lead);

            return FormatarRetorno(leadNova);
        }

        [HttpPut("leads/{id}")]
        public async Task<RetornoApi<LeadAtualizadaModel>> Put([FromBody] LeadAtualizacaoModel lead, int id)
        {
            var leadAtualizada = await _leadServico.AtualizarLead(lead, id);

            return FormatarRetorno(leadAtualizada);
        }

        [HttpPatch("leads/{id}/loja/{idloja}")]
        public async Task<RetornoApi<bool>> AtualizarLoja(int id, int idLoja)
        {
            var leadAtualizada = await _leadServico.AtualizarLojaLead(id, idLoja);

            return FormatarRetorno(leadAtualizada);
        }
    }
}