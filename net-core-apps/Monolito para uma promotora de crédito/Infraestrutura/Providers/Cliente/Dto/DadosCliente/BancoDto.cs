namespace Infraestrutura.Providers.Cliente.Dto
{
    public class BancoDto
    {
        public string Banco { get; set; }

        public int TipoBanco { get; set; }

        public string TipoConta { get; set; }

        public string TipoContaDescricao { get; set; }

        public string Agencia { get; set; }

        public AgenciaCorreioDto AgenciaCorreio { get; set; }

        public string Conta { get; set; }

        public string NomeBanco { get; set; }

        public string PermiteOrdemPagto { get; private set; }

        public string ContaMascara { get; private set; }
    }
}
