namespace Infraestrutura.Autenticacao
{
    public class ConfiguracaoAutenticacao
    {
        public string ChaveJwt { get; set; }

        public int SegundosParaExpirarToken { get; set; }

        public int SegundosParaAtualizarToken { get; set; }
    }
}
