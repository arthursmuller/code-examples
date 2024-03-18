namespace Aplicacao.Model.Autenticacao
{
    public class ValidacaoTokenModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }

        public ValidacaoTokenModel(string login, string email, string nome)
        {
            Login = login;
            Email = email;
            Nome = nome;
        }
    }
}
