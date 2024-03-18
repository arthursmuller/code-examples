namespace Dominio
{
    public class SeguroCoberturaIcatuDominio : EntidadeBase
    {
        public int CodigoCobertura { get; set; }
        public char Tipo { get; set; }
        public decimal ValorCapital { get; set; }
        public decimal ValorPremio { get; set; }
        public char TipoProponente { get; set; }

        public int IdSeguroProdutoIcatu { get; private set; }
        public SeguroProdutoIcatuDominio SeguroProdutoIcatu { get; private set; }

        public SeguroCoberturaIcatuDominio() { }

        public SeguroCoberturaIcatuDominio(int codigoCobertura, char tipo, decimal valorCapital, decimal valorPremio, char tipoProponente, int idSeguroProdutoIcatu)
        {
            CodigoCobertura = codigoCobertura;
            Tipo = tipo;
            ValorCapital = valorCapital;
            ValorPremio = valorPremio;
            TipoProponente = tipoProponente;
            IdSeguroProdutoIcatu = idSeguroProdutoIcatu;
        }

    }
}
