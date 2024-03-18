using Aplicacao.Interfaces;
using Aplicacao.Model.Proposta;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ExcludeFromCodeCoverage]
    public class PropostaController : BaseController
    {
        private readonly IPropostaServico _propostaServico;

        public PropostaController(IBemMensagens mensagens, IPropostaServico propostaServico) : base(mensagens)
            => _propostaServico = propostaServico;

        [HttpGet("clientes/{cpf}/consultas-proposta/{token}/situacao")]
        public async Task<RetornoApi<SituacaoPropostaModel>> ObterSituacaoProposta([FromRoute] string cpf, [FromRoute] string token, [FromQuery][Required] DateTime dataNascimento)
            => FormatarRetorno(await _propostaServico.ObterSituacaoProposta(cpf, token, dataNascimento));
    }
}
