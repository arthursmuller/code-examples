using BrDateTimeUtils;
using ExternalEntities.Domain.Abstractions;
using ExternalEntities.Domain.AggregatesModel.UserAggregate;
using System;

namespace ExternalEntities.Domain.AggregatesModel.BusinessAggregate
{
    public class AnalysisRequest : BaseEntity
    {
        public string Cpf { get; set; }
        public bool IsCompleted { get; set; }
        public decimal Cost { get; set; }
        
        public Business Business { get; private set; }
        private int _businessId;

        private AnalysisRequest() { }
        public AnalysisRequest(string cpf, bool isCompleted, int businessId) 
        {
            Cpf = cpf;
            IsCompleted = isCompleted;
            _businessId = businessId;
        }

        public bool ShouldCharge(int maxHours = 24 * 1) => (DateTime.Now.Brasilia() - _createdDate).TotalHours > maxHours;
    }
}
