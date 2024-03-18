using System.Diagnostics.CodeAnalysis;

namespace LoginSocialFacebook
{
    [ExcludeFromCodeCoverage]
    public class DadosUsuario
    {
        public string Email { get; private set; }
        public string Nome { get; private set; }

        public DadosUsuario(string email, string nome)
        {
            Email = email;
            Nome = nome;
        }
    }
}
