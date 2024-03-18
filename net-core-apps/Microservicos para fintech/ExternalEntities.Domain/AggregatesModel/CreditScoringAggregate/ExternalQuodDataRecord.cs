using BrDateTimeUtils;
using System;

namespace ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate
{
    public class ExternalQuodDataRecord
    {
        public ExternalQuodDataRecord(ExternalQuodData externalQuodData)
        {
            ExternalQuodData = externalQuodData;
            CreatedDate = DateTime.Now.Brasilia();
        }

        private ExternalQuodDataRecord() { }

        public ExternalQuodData ExternalQuodData { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
