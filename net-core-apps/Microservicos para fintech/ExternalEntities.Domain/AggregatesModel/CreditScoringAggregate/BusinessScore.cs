namespace ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate
{
    public class BusinessScore : ScoreBase
    {
        private BusinessScore() { }
        public BusinessScore(int score, string segment, int paymentCommitmentScore, int profileScore) 
            : base(score, segment, paymentCommitmentScore, profileScore) { }
    }
}
