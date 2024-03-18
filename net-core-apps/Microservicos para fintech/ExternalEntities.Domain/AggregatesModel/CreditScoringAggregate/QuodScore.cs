namespace ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate
{
    public class ExternalQuodScore : ScoreBase
    {
        private ExternalQuodScore() { }
        public ExternalQuodScore(int score, string segment, int? paymentCommitmentScore, int? profileScore) : base(score, segment, paymentCommitmentScore, profileScore) { }
    }
}
