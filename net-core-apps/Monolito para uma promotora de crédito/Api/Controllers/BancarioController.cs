using Aplicacao.Model.Banco;
using Aplicacao.Model.TipoConta;
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
    [Route("bancarios")]
    [ExcludeFromCodeCoverage]
    public class BancarioController : BaseController
    {
        private readonly BancarioServico _bancarioServico;

        public BancarioController(BancarioServico bancarioServico, IBemMensagens mensagens) : base(mensagens)
            => _bancarioServico = bancarioServico;

        [HttpGet("tipos-conta")]
        public async Task<RetornoApi<IEnumerable<TipoContaModel>>> Get()
        {
            var tiposConta = await _bancarioServico.ListarTiposConta();

            return FormatarRetorno(tiposConta);
        }

        [HttpGet("bancos")]
        public async Task<RetornoApi<IEnumerable<BancoModel>>> ListarBancos()
        {
            var bancos = await _bancarioServico.ListarBancos();

            return FormatarRetorno(bancos);
        }
    }
}
