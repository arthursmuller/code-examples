using System.Collections.Generic;

namespace ExternalEntities.Domain.Dtos
{
    public class PayiedAnalysisDto
    {
        public int UserId { get; set; }
        public int BusinessId { get; set; }
        public bool Score { get; set; }
        public bool Complete { get; set; }
        public bool Basic { get; set; }
    }

    public record AnalysisResDto(GetScoreDto score ,FinancialDto Financial, UserDto Basic, AnalysisDashboardRes CompleteAnalysis);
    public record FinancialDto(
        int? PaymentCommitmentScore,
        decimal? TotalDelinquencyAmount,
        decimal? TotalCurrentDebitAmount,
        decimal? TotalCurrentDebitDelinquentAmount,
        decimal? TotalOverdrawnAmount,
        decimal? TotalExternalCreditAvailable,
        int? TotalFinancialOperations,
        bool IsDenied);


    public class AnalysisDashboardDto
    {
        public AnalysisDashboardDto(string Name, string Value, string Message, string Status, string Type,string Description)
        {
            this.Name = Name;
            this.Value = Value;
            this.Message = Message;
            this.Status = Status;
            this.Type = Type;
            this.Description = Description;
        }

        public string Name;
        public string Description;
        public string Value;
        public string Message;
        public string Status;
        public string Type;
    }


    public record AnalysisDashboardRes(AnalysisDashboardDto[] Analysis, AnalsysDashbaordStatusDto[] Types);

    public record AnalsysDashbaordStatusDto(string Name, string Description, string OverallStatus);
}
