using BrDateTimeUtils;
using ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate;
using ExternalEntities.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExternalEntities.Domain.AggregatesModel.BusinessAggregate
{
    public class Business : BaseEntity, IAggregateRoot
    {
        public string Cnpj { get; set; }
        public IReadOnlyCollection<BusinessOwner> Owners => _owners;
        private List<BusinessOwner> _owners;

        public IReadOnlyCollection<BusinessScore> Scores => _scores;
        private List<BusinessScore> _scores;

        public IReadOnlyCollection<AnalysisRequest> PayiedAnalysisRequests => _payiedAnalysisRequests;
        private List<AnalysisRequest> _payiedAnalysisRequests;

        private Business() { }
        public Business(int id, string cnpj,  int[] userIds) : base(id)
        {
            Cnpj = cnpj;
            UpdateOwners(userIds);
        }

        public void UpdateOwners(int[] userIds)
        {
            var owners = new List<BusinessOwner>();

            foreach (var userId in userIds)
                owners.Add(new BusinessOwner(userId));

            _owners = owners;
        }
        public bool ShouldSearchScore(int? maxHours = 36) 
        {
            var lastScoreCheckDate = _scores.OrderByDescending(e => e?.CreatedDate)?.FirstOrDefault()?.CreatedDate;
            return (DateTime.Now.Brasilia() - lastScoreCheckDate)?.TotalHours > maxHours || (_scores?.Count == 0);
        }
        public void AddScore(int score, string segment, int paymentCommitmentScore, int profileScore)
        {
            _scores = _scores ?? new List<BusinessScore>();
            _scores.Add(new BusinessScore(score, segment, paymentCommitmentScore, profileScore));
        }

        public void AddPayiedScore(AnalysisRequest analysis)
        {
            _payiedAnalysisRequests = _payiedAnalysisRequests ?? new List<AnalysisRequest>();
            _payiedAnalysisRequests.Add(analysis);
        }
        public BusinessScore GetCurrentScore() => (_scores ?? new List<BusinessScore>())?.OrderByDescending(e => e?.CreatedDate)?.FirstOrDefault();
    }
}
