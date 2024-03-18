using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using B.Web.Controllers;
using B.Mensagens.Interfaces;
using B.Models;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Aplicacao.Interfaces;

namespace Api.Controllers
{
    [Route("/clientes/autenticado/clube-beneficio")]
    [ExcludeFromCodeCoverage]
    [Authorize]
    public class ClubeBeneficiosController : BaseController
    {
        private readonly IClubeBeneficioServico _servico;
        public ClubeBeneficiosController(IClubeBeneficioServico servico, IBemMensagens mensagens) : base(mensagens)
            => _servico = servico;

        [HttpGet]
        public async Task<RetornoApi<string>> Consultarlink()
           => FormatarRetorno(await _servico.CriarAutenticarUsuario());
    }
}
