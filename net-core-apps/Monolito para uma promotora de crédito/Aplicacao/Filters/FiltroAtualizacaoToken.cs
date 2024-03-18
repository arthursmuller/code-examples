using B.Autenticacao;
using Infraestrutura.Autenticacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Aplicacao.Filters
{
    public class FiltroAtualizacaoToken : IAuthorizationFilter
    {
        private readonly IServicoToken _servicoToken;
        private readonly ConfiguracaoAutenticacao _configuracaoAutenticacao;

        public FiltroAtualizacaoToken(IServicoToken servicoToken, ConfiguracaoAutenticacao configuracaoAutenticacao)
        {
            _servicoToken = servicoToken;
            _configuracaoAutenticacao = configuracaoAutenticacao;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (permiteAnonimo(context))
                return;

            string token = context.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                token = token.Replace("Bearer ", "");

                JwtSecurityToken jwtToken = new JwtSecurityToken(token);

                DateTime dataExpiracaoToken = jwtToken.ValidTo.ToLocalTime();

                if (dataExpiracaoToken.Subtract(DateTime.Now).TotalSeconds <= _configuracaoAutenticacao.SegundosParaAtualizarToken)
                {
                    var novoToken = _servicoToken.GerarToken(context.HttpContext.User.Claims, _configuracaoAutenticacao.ChaveJwt, TimeSpan.FromSeconds(_configuracaoAutenticacao.SegundosParaExpirarToken));
                    context.HttpContext.Response.Headers.Add("Authorization", novoToken);
                }
            }
        }

        private static bool permiteAnonimo(AuthorizationFilterContext context)
        {
            return (context.ActionDescriptor as ControllerActionDescriptor)
                                .MethodInfo
                                .CustomAttributes
                                .Any(m => m.AttributeType == typeof(AllowAnonymousAttribute));
        }
    }
}
