using Aplicacao.Servico;
using Dominio;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Filters
{
    public class FiltroLogRequisicao : ActionFilterAttribute
    {
        private readonly IUsuarioLogin _usuarioLogin;
        private readonly IUsuarioServico _usuarioServico;

        public FiltroLogRequisicao(IUsuarioLogin usuarioLogin, IUsuarioServico usuarioServico)
        {
            _usuarioLogin = usuarioLogin;
            _usuarioServico = usuarioServico;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var urlRequisicao = obterUrlRequisicao(context);
            var corpoRequisicao = await obterCorpoRequisicao(context);

            await _usuarioServico.GravarLogRequisicao(new UsuarioRequisicaoLogDominio(_usuarioLogin.IdUsuario, _usuarioLogin.UsuarioTenant, urlRequisicao, corpoRequisicao));

            await base.OnActionExecutionAsync(context, next);
        }

        private string obterUrlRequisicao(ActionExecutingContext context)
            => $"{context.HttpContext.Request.Host.Value}{context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString.Value}";

        private async Task<string> obterCorpoRequisicao(ActionExecutingContext context)
        {
            StreamReader reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8);
            var corpoRequisicao = await reader.ReadToEndAsync();

            return string.IsNullOrWhiteSpace(corpoRequisicao) ? null : corpoRequisicao;
        }
    }
}
