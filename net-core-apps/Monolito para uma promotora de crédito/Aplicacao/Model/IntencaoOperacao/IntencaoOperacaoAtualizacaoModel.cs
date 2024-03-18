using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.IntencaoOperacao
{
    [ExcludeFromCodeCoverage]
    public class IntencaoOperacaoAtualizacaoModel : IntencaoOperacaoCriacaoModel
    {
        public int? IdLoja { get; set; }
        public int? IdLead { get; set; }
    }
}
