namespace Infraestrutura.Providers.Auth.Dto
{
    public class RetornoAtenticacaoDto
    {
        public string JwtToken { get; set; }
        public string BemWebToken { get; set; }
        public bool RedefinirSenha { get; set; }
    }
}
