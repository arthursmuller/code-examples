namespace Dominio
{
    public class SeguroSituacaoIcatuDominio : EntidadeBase
    {
        public string Status { get; set; }
        public int? IdSeguroPropostaIcatu { get; private set; }
        public SeguroPropostaIcatuDominio SeguroPropostaIcatu { get; private set; }
    }
}
