namespace ExternalEntities.Domain.Dtos
{
    public class ScoreDto
    {
        public int? Score { get; set; }
        public string Segment { get; set; }
        public int? PaymentCommitmentScore { get; set; }
        public int? ProfileScore { get; set; }
        public ScoreDto() { }
        public ScoreDto(int? score, string segment, int? paymentCommitmentScore, int? profileScore)
        {
            Score = score;
            Segment = segment;
            PaymentCommitmentScore = paymentCommitmentScore;
            ProfileScore = profileScore;
        }
    }
}
