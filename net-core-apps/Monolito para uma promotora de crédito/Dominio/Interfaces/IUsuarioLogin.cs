namespace Dominio
{
    public interface IUsuarioLogin
    {
        public int IdUsuario { get; set; }

        public string Nome { get; set; }

        public string UsuarioTenant { get; set; }

        public string EnderecoIpOrigemRequisicao { get; set; }

        public string AccessToken { get; set; }
        string BuscarEnderecoIp();
    }
}
