using Aplicacao.Model.Usuario;
using System.ComponentModel.DataAnnotations;

namespace Aplicacao
{
    public class UsuarioCriacaoModel
    {
        [Required]
        public string Nome { get; set; }

        private string _email;
        public string Email { get => _email; set => _email = value?.Trim().ToLower(); }

        public string CPF { get; set; }

        public UsuarioLoginSocialCriacaoModel LoginSocial { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
