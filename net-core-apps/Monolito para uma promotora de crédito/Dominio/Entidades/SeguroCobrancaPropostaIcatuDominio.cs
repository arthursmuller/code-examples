using System;

namespace Dominio
{
    public class SeguroCobrancaPropostaIcatuDominio : EntidadeBase
    {
        public DateTime DataVencimento { get; set; }
        public int IdConvenio { get; set; }
        public string LinkPagamento { get; set; }
        public string IdPedidoPagamento { get; set; }
        public string IdLinkPagamento { get; set; }
        public string IdCobranca { get; set; }
        public int? IdSeguroPropostaIcatu { get; private set; }
        public SeguroPropostaIcatuDominio SeguroPropostaIcatu { get; private set; }

        public SeguroCobrancaPropostaIcatuDominio(int idConvenio, 
            DateTime dataVencimento, 
            string linkPagamento, 
            string idPedidoPagamento, 
            string idLinkPagamento, 
            string idCobranca, 
            int? idSeguroPropostaIcatu)
        {
            IdConvenio = idConvenio;
            DataVencimento = dataVencimento;
            LinkPagamento = linkPagamento;
            IdPedidoPagamento = idPedidoPagamento;
            IdLinkPagamento = idLinkPagamento;
            IdCobranca = idCobranca;
            IdSeguroPropostaIcatu = idSeguroPropostaIcatu;
        }
    }
}
