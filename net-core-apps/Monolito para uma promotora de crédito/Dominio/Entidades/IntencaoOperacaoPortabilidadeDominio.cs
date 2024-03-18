namespace Dominio
{
    public class IntencaoOperacaoPortabilidadeDominio : EntidadeBase
    {
        public int IdIntencaoOperacao { get; private set; }
        public int IdBancoOriginador { get; private set; }
        public int PrazoRestante { get; private set; }
        public int PrazoTotal { get; private set; }
        public decimal SaldoDevedor { get; private set; }
        public string PlanoRefinanciamento { get; private set; }
        public int? PrazoRefinanciamento { get; private set; }
        public decimal? ValorPrestacaoRefinanciamento { get; private set; }

        public BancoDominio BancoOriginador { get; private set; }
        public IntencaoOperacaoDominio IntencaoOperacao { get; private set; }

        protected IntencaoOperacaoPortabilidadeDominio() { }

        public IntencaoOperacaoPortabilidadeDominio(int idBancoOriginador, int prazoRestante, int prazoTotal, decimal saldoDevedor, string planoRefin,
            int? prazoRefinanciamento, decimal? valorPrestacaoRefinanciamento)
        {
            IdBancoOriginador = idBancoOriginador;
            PrazoRestante = prazoRestante;
            PrazoTotal = prazoTotal;
            SaldoDevedor = saldoDevedor;
            PlanoRefinanciamento = planoRefin;
            PrazoRefinanciamento = prazoRefinanciamento;
            ValorPrestacaoRefinanciamento = valorPrestacaoRefinanciamento;
        }

        public void SetPropriedadesAtualizadas(int idBancoOriginador, int prazoRestante, int prazoTotal, decimal saldoDevedor, string planoRefin,
            int? prazoRefinanciamento, decimal? valorPrestacaoRefinanciamento)
        {
            IdBancoOriginador = idBancoOriginador;
            PrazoRestante = prazoRestante;
            PrazoTotal = prazoTotal;
            SaldoDevedor = saldoDevedor;
            PlanoRefinanciamento = planoRefin;
            PrazoRefinanciamento = prazoRefinanciamento;
            ValorPrestacaoRefinanciamento = valorPrestacaoRefinanciamento;

            setDataAtualizacao();
        }
    }
}
