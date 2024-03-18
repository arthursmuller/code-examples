using Aplicacao.Model.Autenticacao;
using Aplicacao.Servico;
using B.Mensagens.Interfaces;
using B.Models;
using B.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ExcludeFromCodeCoverage]
    public class LoginController : BaseController
    {
        private readonly IAutenticacaoServico _autenticacaoServico;
        private readonly IAutenticacaoLoginSocialServico _autenticacaoLoginSocialServico;

        public LoginController(IAutenticacaoServico autenticacaoServico, IAutenticacaoLoginSocialServico autenticacaoLoginSocialServico, IBemMensagens mensagens) : base(mensagens)
        {
            _autenticacaoServico = autenticacaoServico;
            _autenticacaoLoginSocialServico = autenticacaoLoginSocialServico;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<RetornoApi<AutenticacaoModel>> Login([FromBody] LoginModel model)
        {
            var autenticacao = await _autenticacaoServico.Autenticar(model);

            return FormatarRetorno(autenticacao);
        }

        [HttpPost("login/administrador")]
        [AllowAnonymous]
        public async Task<RetornoApi<AutenticacaoModel>> LoginTenant([FromBody] LoginModel model)
        {
            var autenticacao = await _autenticacaoServico.Autenticar(model, administrador: true);

            return FormatarRetorno(autenticacao);
        }

        [HttpPost("login-social/{redeSocial}")]
        [AllowAnonymous]
        public async Task<RetornoApi<AutenticacaoLoginSocialModel>> LoginSocial([FromRoute] int redeSocial, [FromBody] LoginSocialEnvioModel model)
        {
            var autenticacao = await _autenticacaoLoginSocialServico.Autenticar(new LoginSocialModel((Dominio.Enum.RedeSocial)redeSocial, model.Token, model.Code, model.RedirectURL));

            return FormatarRetorno(autenticacao);
        }
    }
}
