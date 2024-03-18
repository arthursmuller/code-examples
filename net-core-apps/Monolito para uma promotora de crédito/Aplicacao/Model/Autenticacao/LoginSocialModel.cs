using Dominio.Enum;

namespace Aplicacao.Model.Autenticacao
{
    public class LoginSocialModel
    {
        public RedeSocial RedeSocial { get; set; }
        public string Token { get; set; }
        public string AppleCode { get; set; }
        public string RedirectUrl { get; set; }


        public LoginSocialModel(RedeSocial redeSocial, string token, string appleCode = null, string redirectUrl = null)
        {
            RedeSocial = redeSocial;
            Token = token;
            AppleCode = appleCode;
            RedirectUrl = redirectUrl;
        }
    }
}
