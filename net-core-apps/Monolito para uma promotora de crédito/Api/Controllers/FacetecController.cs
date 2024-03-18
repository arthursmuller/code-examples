using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using B.Facetec;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("validacoes-biometricas")]
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class FacetecController : BaseController
    {
        private readonly IProvedorFacetec _provedorFacetec;

        public FacetecController(IBemMensagens mensagens, IProvedorFacetec facetecServico) : base(mensagens)
            => _provedorFacetec = facetecServico;

        [HttpPost("liveness")]
        public async Task<RetornoApi<LivenessRetorno>> ObterLiveness([FromBody] LivenessFacetec parametros)
        {
            var liveness = await _provedorFacetec.ObterLiveness(parametros, obterUsuarioAgenteFacetec());
            return FormatarRetorno(liveness);
        }

        [HttpPost("facematch")]
        public async Task<RetornoApi<FacematchRetorno>> ObterLivenessFacematch([FromBody] string facematch)
        {
            var resultado = await _provedorFacetec.ObterLivenessFacematch(facematch, obterUsuarioAgenteFacetec());
            return FormatarRetorno(resultado);
        }

        [HttpGet("token")]
        public async Task<RetornoApi<SessaoRetorno>> ObterTokenSessao()
        {
            var tokenSessao = await _provedorFacetec.ObterTokenSessao(obterUsuarioAgenteFacetec());
            return FormatarRetorno(tokenSessao);
        }

        [HttpPost("inscricao")]
        public async Task<RetornoApi<InscricaoRetorno>> ObterInscricao([FromBody] string inscricao)
        {
            var resultado = await _provedorFacetec.ObterInscricao(inscricao, obterUsuarioAgenteFacetec());
            return FormatarRetorno(resultado);
        }

        [HttpPost("liveness-2d")]
        public async Task<RetornoApi<Liveness2DRetorno>> ObterLiveness2D([FromBody] byte[] base64Imagem)
        {
            var liveness = await _provedorFacetec.ObterLiveness2D(base64Imagem);
            return FormatarRetorno(liveness);
        }

        private string obterUsuarioAgenteFacetec()
        {
            HttpContext.Request.Headers.TryGetValue("X-User-Agent", out var usuarioAgenteFacetec);
            return usuarioAgenteFacetec;
        }
    }
}
