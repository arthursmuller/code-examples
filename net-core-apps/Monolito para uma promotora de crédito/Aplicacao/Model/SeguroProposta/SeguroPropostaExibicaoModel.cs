using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacao.Model.SeguroProposta
{
    public class SeguroPropostaExibicaoModel
    {
        public DateTime DataAssinatura { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFimVigencia { get; set; }
        public DateTime DataProtocolo { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorPremioTotal { get; set; }
        public bool ValorPrimeiroPremioPago { get; set; }
        public long NumeroPropostaIcatu { get; set; }
        public int IdSeguroProduto { get; set; }
        public int IdCliente { get; set; }

        public SeguroPropostaExibicaoModel(DateTime dataAssinatura, DateTime dataInicioVigencia, DateTime dataFimVigencia, DateTime dataProtocolo, DateTime dataVencimento, decimal valorPremioTotal, bool valorPrimeiroPremioPago, long numeroPropostaIcatu, int idSeguroProduto, int idCliente)
        {
            DataAssinatura = dataAssinatura;
            DataInicioVigencia = dataInicioVigencia;
            DataFimVigencia = dataFimVigencia;
            DataProtocolo = dataProtocolo;
            DataVencimento = dataVencimento;
            ValorPremioTotal = valorPremioTotal;
            ValorPrimeiroPremioPago = valorPrimeiroPremioPago;
            NumeroPropostaIcatu = numeroPropostaIcatu;
            IdSeguroProduto = idSeguroProduto;
            IdCliente = idCliente;
        }
    }
}
