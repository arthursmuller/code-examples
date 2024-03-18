using ExternalEntities.Domain.Abstractions;
using System.Collections.Generic;

namespace ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate
{
    public class ExternalQuodProtest : ValueObject
    {
        public ExternalQuodProtest(string situacao, string valorProtestadosTotal, string totalProtestos)
        {
            Situacao = situacao;
            ValorProtestadosTotal = valorProtestadosTotal;
            TotalProtestos = totalProtestos;
        }

        private ExternalQuodProtest() { }
        public string Situacao { get; set; }
        public string ValorProtestadosTotal { get; set; }
        public string TotalProtestos { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Situacao;
            yield return TotalProtestos;
            yield return ValorProtestadosTotal;
        }
    }
}
