using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.Autenticacao
{
    [ExcludeFromCodeCoverage]
    public class AutenticacaoLoginSocialModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public bool UsuarioCadastrado { get => !string.IsNullOrWhiteSpace(Token); }

        public AutenticacaoLoginSocialModel() { }

        public AutenticacaoLoginSocialModel(string nome, string email, string token)
        {
            Nome = nome;
            Email = email;
            Token = token;
        }
    }
}
