using System.Diagnostics.CodeAnalysis;

namespace Aplicacao.Model.Proposta
{
    [ExcludeFromCodeCoverage]
    public class SituacaoPropostaModel
    {
        public long Proposta { get; set; }
        public string DescricaoSituacao { get; set; }
        public string ExplicacaoSituacao { get; set; }
    }
}
