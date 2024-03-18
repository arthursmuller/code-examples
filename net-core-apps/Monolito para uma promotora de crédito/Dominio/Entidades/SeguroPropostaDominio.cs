using Dominio.Enum;
using System;

namespace Dominio
{
    public class SeguroPropostaDominio : EntidadeBase
    {
        public DateTime DataAssinatura { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFimVigencia { get; set; }
        public DateTime DataProtocolo { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorPremioTotal { get; set; }
        public bool ValorPrimeiroPremioPago { get; set; }
        public long NumeroPropostaIcatu { get; set; }
        public string UrlPdfContrato { get; private set; }
        public int Longitude { get; private set; }
        public int Latitude { get; private set; }
        public string IPOrigem { get; private set; }
        public MeioPagamentoSeguro IdMeioPagamento { get; private set; }

        public int IdSeguroProduto { get; private set; }
        public SeguroProdutoDominio Produto { get; set; }

        public int IdCliente { get; private set; }
        public ClienteDominio Cliente { get; set; }

        public SeguroPropostaDominio() { }

        public SeguroPropostaDominio(decimal valorPremioTotal, bool valorPrimeiroPremioPago, int idSeguroProduto, int idCliente, MeioPagamentoSeguro meioPagamento)
        {
            DataAssinatura = DateTime.UtcNow;
            DataInicioVigencia = DateTime.UtcNow;
            DataFimVigencia = DateTime.UtcNow.AddYears(10);
            DataProtocolo = DateTime.UtcNow;
            DataVencimento = DateTime.UtcNow;
            ValorPremioTotal = valorPremioTotal;
            ValorPrimeiroPremioPago = valorPrimeiroPremioPago;
            IdSeguroProduto = idSeguroProduto;
            IdCliente = idCliente;
            IdMeioPagamento = meioPagamento;
        }

        public void SetUrlPdfContrato(string url) => UrlPdfContrato = url;
        public void CadastrarAssinaturaDigital(int longitude, int latitude, string ipOrigem)
        {
            Longitude = longitude;
            Latitude = latitude;
            IPOrigem = ipOrigem;
        }
    }
}