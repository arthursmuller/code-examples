using Aplicacao;
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
    public class LojaController : BaseController
    {
        private readonly LojaServico _lojaServico;

        public LojaController(LojaServico lojaServico, IBemMensagens mensagens) : base(mensagens)
        {
            _lojaServico = lojaServico;
        }

        [HttpGet("lojas")]
        public async Task<RetornoApi<IEnumerable<LojaModel>>> Get([FromQuery] bool necessarioContatoWhatsApp = false, [FromQuery] int? idUf = null)
        {
            var lojas = await _lojaServico.BuscarLojas(necessarioContatoWhatsApp, idUf);

            return FormatarRetorno(lojas);
        }

        [HttpPost("lojas")]
        [Authorize(Roles = "admin")]
        public async Task<RetornoApi<LojaModel>> Post([FromBody] LojaCriacaoModel loja)
        {
            var lojaNova = await _lojaServico.CriarLoja(loja);

            return FormatarRetorno(lojaNova);
        }

        [HttpDelete("lojas/{id}")]
        [Authorize(Roles = "admin")]
        public async Task<RetornoApi<bool>> Delete(int id)
        {
            var resultado = await _lojaServico.DeletarLoja(id);

            return FormatarRetorno(resultado);
        }
    }
}
