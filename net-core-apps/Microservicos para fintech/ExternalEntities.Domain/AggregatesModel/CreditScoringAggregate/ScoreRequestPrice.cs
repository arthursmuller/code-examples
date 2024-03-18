using Notifications.Domain.Exceptions;
using RichEnumeration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate
{
    public class AnalysisRequestPrice : Enumeration
    {
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public static AnalysisRequestPrice Score = new AnalysisRequestPrice(1, nameof (Score), "Consulta Score", 5.00m);
        public static AnalysisRequestPrice Complete = new AnalysisRequestPrice(2, nameof(Complete), "Consulta Completa", 8.00m);

        public AnalysisRequestPrice(int id, string name, string description, decimal cost) : base(id, name)
            => (Description, Cost) = (description, cost);

        public static IEnumerable<AnalysisRequestPrice> List() =>
               new[] { Score, Complete };

        public static AnalysisRequestPrice FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
                throw new ExternalEntitiesException($"Possible values for AnalysisRequestPrice: {String.Join(",", List().Select(s => s.Name))}");

            return state;
        }

        public static AnalysisRequestPrice From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
                throw new ExternalEntitiesException($"Possible values for AnalysisRequestPrice: {String.Join(",", List().Select(s => s.Name))}");

            return state;
        }
    }
}
