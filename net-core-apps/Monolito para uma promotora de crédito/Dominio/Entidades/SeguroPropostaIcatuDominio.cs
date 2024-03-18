using System;

namespace Dominio
{
    public class SeguroPropostaIcatuDominio : EntidadeBase
    {
        public DateTime DataAssinatura { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFimVigencia { get; set; }
        public DateTime DataProtocolo { get; set; }
        public decimal ValorPremioTotal { get; set; }
        public bool ValorPrimeiroPremioPago { get; set; }
        public long NumeroPropostaIcatu { get; set; }
        public int? IdSeguroProposta { get; private set; }
        public SeguroPropostaDominio SeguroProposta { get; private set; }
        public int? IdSeguroClienteIcatu { get; private set; }
        public SeguroClienteIcatuDominio SeguroClienteIcatu { get; private set; }

        public SeguroPropostaIcatuDominio() { }

        public SeguroPropostaIcatuDominio(DateTime dataAssinatura, DateTime dataInicioVigencia, DateTime dataFimVigencia, DateTime dataProtocolo, DateTime dataVencimento, decimal valorPremioTotal, bool valorPrimeiroPremioPago, int idSeguroProposta, int idSeguroClienteIcatu)
        {
            DataAssinatura = dataAssinatura;
            DataInicioVigencia = dataInicioVigencia;
            DataFimVigencia = dataFimVigencia;
            DataProtocolo = dataProtocolo;
            ValorPremioTotal = valorPremioTotal;
            ValorPrimeiroPremioPago = valorPrimeiroPremioPago;
            IdSeguroProposta = idSeguroProposta;
            IdSeguroClienteIcatu = idSeguroClienteIcatu;
        }
    }
}
