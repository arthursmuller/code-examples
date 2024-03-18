using Dominio.Enum;

namespace Aplicacao.Model.Usuario
{
    public class UsuarioLoginSocialCriacaoModel
    {
        public RedeSocial RedeSocial { get; set; }
        public string Token { get; set; }
    }
}
