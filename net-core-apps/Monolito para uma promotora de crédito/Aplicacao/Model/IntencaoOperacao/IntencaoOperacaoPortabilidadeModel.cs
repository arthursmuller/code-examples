using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.IntencaoOperacao
{
    [ExcludeFromCodeCoverage]
    public class IntencaoOperacaoPortabilidadeModel
    {
        public int IdBancoOriginador { get; set; }
        public int PrazoRestante { get; set; }
        public int PrazoTotal { get; set; }
        public decimal SaldoDevedor { get; set; }
        public string PlanoRefinanciamento { get; set; }
        public int? PrazoRefinanciamento { get; set; }
        public decimal? ValorPrestacaoRefinanciamento { get; set; }
    }
}
