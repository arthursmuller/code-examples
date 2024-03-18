using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Model.Autenticacao
{
    public class LoginSocialEnvioModel
    {
        [Required]
        public string Token { get; set; }

        public string Code { get; set; }

        public string RedirectURL { get; set; }

    }
}
