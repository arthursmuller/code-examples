using ExternalEntities.Domain.Abstractions;

namespace ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate
{
    public abstract class ScoreBase : BaseEntity
    {
        public int Score { get; set; }
        public string Segment { get; set; }
        public int? PaymentCommitmentScore { get; set; }
        public int? ProfileScore { get; set; }
        public ScoreBase() { }
        public ScoreBase(int score, string segment, int? paymentCommitmentScore, int? profileScore)
        {
            Score = score;
            Segment = segment;
            PaymentCommitmentScore = paymentCommitmentScore;
            ProfileScore = profileScore;
        }
    }
}
