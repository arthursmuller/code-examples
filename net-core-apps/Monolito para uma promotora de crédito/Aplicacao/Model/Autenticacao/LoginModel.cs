using Dominio.Enum;

namespace Aplicacao.Model.Autenticacao
{
    public class LoginModel
    {
        private string _email;
        public string Email { get => _email; set => _email = value?.ToLower(); }
       
        public string Cpf { get; set; }

        public string Senha { get; set; }

        public string UsuarioTenant { get; set; }
    }
}
