namespace Dominio
{
    public class SeguroCobrancaPropostaCartaoIcatuDominio : EntidadeBase
    {
        public string IdCartao { get; private set; }
        public string IdCobranca { get; private set; }
        public string Titular { get; private set; }
        public string CpfTitular { get; private set; }
        public string QuatroUltimosDigitosCartao { get; private set; }
        public int? IdSeguroCobrancaPropostaIcatu { get; private set; }
        public SeguroCobrancaPropostaIcatuDominio SeguroCobrancaPropostaIcatu { get; private set; }

        public SeguroCobrancaPropostaCartaoIcatuDominio(string idCartao, string idCobranca, string titular, string cpfTitular, string quatroUltimosDigitosCartao, int? idSeguroCobrancaPropostaIcatu)
        {
            IdCartao = idCartao;
            IdCobranca = idCobranca;
            QuatroUltimosDigitosCartao = quatroUltimosDigitosCartao;
            IdSeguroCobrancaPropostaIcatu = idSeguroCobrancaPropostaIcatu;
            Titular = titular;
            CpfTitular = cpfTitular;
        }
    }
}
