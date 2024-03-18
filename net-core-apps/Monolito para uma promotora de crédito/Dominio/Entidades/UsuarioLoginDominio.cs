using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Dominio
{
    public class UsuarioLoginDominio : IUsuarioLogin
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public int IdUsuario { get; set; }

        public string Nome { get; set; }

        public string UsuarioTenant { get; set; }

        public string EnderecoIpOrigemRequisicao { get; set; }

        public string AccessToken { get; set; }

        public UsuarioLoginDominio() { }

        public UsuarioLoginDominio(IHttpContextAccessor httpContextAccessor)
        {
            IdUsuario = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Nome = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            UsuarioTenant = httpContextAccessor.HttpContext.User.FindFirst("UsuarioTenant")?.Value;
            EnderecoIpOrigemRequisicao = httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            AccessToken = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Replace("Bearer", "");
            _httpContextAccessor = httpContextAccessor;
        }
        
        public string BuscarEnderecoIp() => EnderecoIpOrigemRequisicao;

    }
}