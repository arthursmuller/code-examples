using ExternalEntities.Domain.AggregatesModel.UserAggregate;
using ExternalEntities.Domain.AggregatesModel.BusinessAggregate;
using ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate;
using ExternalEntities.Domain.AggregatesModel.KpiAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System;

namespace ExternalEntities.Domain.Dtos
{
    public static class DtoConversor
    {
        public static ApplicationUserDto ConvertToUserDto(ApplicationUser user) =>
            new ApplicationUserDto(
                user?.Id,
                user?.Cpf);

        public static BusinessDto ConvertToBusinessDto(Business business) =>
            new BusinessDto(
                business?.Id,
                business?.Cnpj);
        public static GetScoreDto Convert(ApplicationUser user) =>
            new GetScoreDto(user.Id, user?.GetScore(), (user?.TotalExternalCreditAvailable ?? 0) + (user?.TotalCurrentDebitAmount ?? 0), user?.Cpf);
        public static FinancialDto FinancialDto(ApplicationUser user) =>
             new FinancialDto(
                    user?.GetCurrentQuodData()?.Score?.PaymentCommitmentScore,
                    user.TotalDelinquencyAmount,
                    user.TotalCurrentDebitAmount,
                    user.TotalCurrentDebitDelinquentAmount,
                    user.TotalOverdrawnAmount,
                    user.TotalExternalCreditAvailable,
                    user.TotalFinancialOperations,
                    user.IsDenied);

        public static GetScoreDto ConvertForSimulation(ApplicationUser user) =>
            new GetScoreDto(user.Id, user?.GetScore(), (user?.TotalExternalCreditAvailable ?? 999999999) + (user?.TotalCurrentDebitAmount ?? 0), user?.Cpf);
        public static ScoreDto ConvertToScoreDto(ScoreBase score) =>
            new ScoreDto(
                score?.Score,
                score?.Segment,
                score?.PaymentCommitmentScore,
                score?.ProfileScore);

        public static AnalysisDashboardRes AnalysisDashboardDto(ApplicationUser user)
        {
            var res = new List<AnalysisDashboardDto>();
            var totalInsightValues = 0;
            var totalFlagsValues = 0;

            foreach (var item in AnalysisDashboardField.List())
            {
                var (value, type, name, msg, status) = ("", "", "", "", "");

                if (item.Id == AnalysisDashboardField.GeneralAnalysis.Id)
                {
                    var score = user?.GetScore();
                    (value, type, name, (msg, status)) =
                    (
                        $"{score}",
                        AnalysisDashboardField.GeneralAnalysis.Type,
                        AnalysisDashboardField.GeneralAnalysis.Name,
                        (
                            score > 800 ?
                                (AnalysisDashboardField.GeneralAnalysis.MsgBom, "Bom") :
                            score > 600 ?
                                (AnalysisDashboardField.GeneralAnalysis.MsgMedio, "Médio") : (AnalysisDashboardField.GeneralAnalysis.MsgRuim, "Ruim")
                        )
                    );
                }
                if (item.Id == AnalysisDashboardField.PaymentCommitmentScore.Id)
                {
                    var paymentCommitmentScore = user?.GetCurrentQuodData()?.Score?.PaymentCommitmentScore;
                    
                    (type, value, name, (msg, status)) =
                    (
                        $"{paymentCommitmentScore}",
                        AnalysisDashboardField.PaymentCommitmentScore.Type,
                        AnalysisDashboardField.PaymentCommitmentScore.Name,
                        (
                            paymentCommitmentScore >= 85 ?
                                (AnalysisDashboardField.PaymentCommitmentScore.MsgBom, "Bom") :
                            paymentCommitmentScore >= 70 ?
                                (AnalysisDashboardField.PaymentCommitmentScore.MsgMedio, "Médio") : (AnalysisDashboardField.PaymentCommitmentScore.MsgRuim, "Ruim")
                        )
                    );
                }
                if (item.Id == AnalysisDashboardField.AnalysisTrustAndConfiability.Id)
                {
                    var segmentScore = NumberHelper.TryParseInt(user?.GetCurrentQuodData()?.Score?.Segment) ?? 0;

                    (value, type, name, (msg, status)) =
                    (
                        $"{segmentScore}",
                        AnalysisDashboardField.AnalysisTrustAndConfiability.Type,
                        AnalysisDashboardField.AnalysisTrustAndConfiability.Name,
                        (
                            segmentScore >= 5 ?
                                (AnalysisDashboardField.AnalysisTrustAndConfiability.MsgBom, "Bom") :
                            segmentScore >= 3 ?
                                (AnalysisDashboardField.AnalysisTrustAndConfiability.MsgMedio, "Médio") : (AnalysisDashboardField.AnalysisTrustAndConfiability.MsgRuim, "Ruim")
                        )
                    );
                }
                if (item.Id == AnalysisDashboardField.QualityOfFinancialOperations.Id)
                {
                    var numberOfOperations = user?.TotalFinancialOperations ?? 0;
                    (value, type, name, (msg, status)) =
                    (
                        $"{numberOfOperations}",
                        AnalysisDashboardField.QualityOfFinancialOperations.Type,
                        AnalysisDashboardField.QualityOfFinancialOperations.Name,
                        (
                            numberOfOperations >= 4 ?
                                (AnalysisDashboardField.QualityOfFinancialOperations.MsgBom, "Bom") :
                            numberOfOperations >= 2 ?
                                (AnalysisDashboardField.QualityOfFinancialOperations.MsgMedio, "Médio") : (AnalysisDashboardField.QualityOfFinancialOperations.MsgRuim, "Ruim")
                        )
                    );
                }
                if (item.Id == AnalysisDashboardField.ProfileScore.Id)
                {
                    var profileScore = user?.GetCurrentQuodData()?.Score?.ProfileScore ?? 0;
                    (value, type, name, (msg, status)) =
                    (
                        $"{profileScore}",
                        AnalysisDashboardField.ProfileScore.Type,
                        AnalysisDashboardField.ProfileScore.Name,
                        (
                            profileScore > 80 ?
                                (AnalysisDashboardField.ProfileScore.MsgBom, "Bom") :
                            profileScore >= 60 ?
                                (AnalysisDashboardField.ProfileScore.MsgMedio, "Médio") : (AnalysisDashboardField.ProfileScore.MsgRuim, "Ruim")
                        )
                    );
                }
                if (item.Id == AnalysisDashboardField.ActiveProtests.Id)
                {
                    var numerOfProtests = user?.GetCurrentQuodData()?.Protests?.Count ?? 0;
                    (value, type, name, (msg, status)) =
                    (
                        $"{numerOfProtests}",
                        AnalysisDashboardField.ActiveProtests.Type,
                        AnalysisDashboardField.ActiveProtests.Name,
                        (
                            numerOfProtests < 1 ?
                                (AnalysisDashboardField.ActiveProtests.MsgBom, "Bom") :
                            numerOfProtests <= 3 ?
                                (AnalysisDashboardField.ActiveProtests.MsgMedio, "Médio") : (AnalysisDashboardField.ActiveProtests.MsgRuim, "Ruim")
                        )
                    );
                }
                if (item.Id == AnalysisDashboardField.ActiveDebtsAmount.Id)
                {
                    var activeDebtsAmount = (user?.GetCurrentQuodData()?.Negative?.PendenciesControlCred + user?.TotalDelinquencyAmount) ?? 0;
                    var percentage = (activeDebtsAmount / user?.TotalCurrentDebitAmount) ?? 0;

                    (value, type, name, (msg, status)) =
                    (
                        getCurrency(activeDebtsAmount),
                        AnalysisDashboardField.ActiveDebtsAmount.Type,
                        AnalysisDashboardField.ActiveDebtsAmount.Name,
                        (
                            activeDebtsAmount == 0 ?
                                (AnalysisDashboardField.ActiveDebtsAmount.MsgBom, "Bom") :
                            (percentage < 10 && percentage > 0) ?
                                (AnalysisDashboardField.ActiveDebtsAmount.MsgMedio, "Médio") : (AnalysisDashboardField.ActiveDebtsAmount.MsgRuim, "Ruim")
                        )
                    );
                }
                if (item.Id == AnalysisDashboardField.LoanAmountSuggested.Id)
                {
                    var amount = user?.TotalCurrentDebitAmount * 5;
                    var suggestedCredit = amount < 1500 ? 1500 : amount > 30000 ? 30000 : amount;
                    if(amount > 0) totalInsightValues++;
                    
                    (value, type, name) =
                    (
                        getCurrency(suggestedCredit),
                        AnalysisDashboardField.LoanAmountSuggested.Type,
                        AnalysisDashboardField.LoanAmountSuggested.Name
                    );
                }
                if (item.Id == AnalysisDashboardField.ActiveLoansAmount.Id)
                {
                    var currentDebitAmount = user?.TotalCurrentDebitAmount ?? 0;
                    if(currentDebitAmount > 0) totalInsightValues++;
                    
                    (value, type, name) =
                    (
                        getCurrency(currentDebitAmount),
                        AnalysisDashboardField.ActiveLoansAmount.Type,
                        AnalysisDashboardField.ActiveLoansAmount.Name
                    );
                }
                if (item.Id == AnalysisDashboardField.CreditLimit.Id)
                {
                    var creditToBeAvailable = user?.TotalExternalCreditAvailable ?? 0;
                    if(creditToBeAvailable > 0) totalInsightValues++;
                    
                    (value, type, name) =
                    (
                        getCurrency(creditToBeAvailable),
                        AnalysisDashboardField.CreditLimit.Type,
                        AnalysisDashboardField.CreditLimit.Name
                    );
                }
                if (item.Id == AnalysisDashboardField.ChequeEspecial.Id && user?.TotalOverdrawnAmount > 0)
                {
                    totalFlagsValues++;

                    (type, name, msg) =
                    (
                        AnalysisDashboardField.ChequeEspecial.Type,
                        AnalysisDashboardField.ChequeEspecial.Name,
                        AnalysisDashboardField.ChequeEspecial.MsgGeral
                    );
                }
                if (item.Id == AnalysisDashboardField.CurrentDefaultDebts.Id && user?.TotalCurrentDebitAmount > 0)
                {
                    totalFlagsValues++;
                    
                    (type, name, msg) =
                    (
                        AnalysisDashboardField.CurrentDefaultDebts.Type,
                        AnalysisDashboardField.CurrentDefaultDebts.Name,
                        AnalysisDashboardField.CurrentDefaultDebts.MsgGeral
                    );
                }

                if(!string.IsNullOrEmpty(msg)
                    || !string.IsNullOrEmpty(value) && value != "null"
                    || !string.IsNullOrEmpty(name))
                {
                    res.Add(new AnalysisDashboardDto(name, value, msg, status, type, null));
                }
            }

            return new AnalysisDashboardRes(
                res.ToArray(), 
                AnalysisDashboardFieldType.List().Select(e =>
                {
                    var overallStatus  = "";
                    if(e.Id == AnalysisDashboardFieldType.Flag.Id)
                    {
                        overallStatus = totalFlagsValues == 0 ? "Bom" : "Ruim";
                    }
                    if (e.Id == AnalysisDashboardFieldType.Indicator.Id)
                    {
                        overallStatus = "";
                    }
                    if (e.Id == AnalysisDashboardFieldType.Insight.Id)
                    {
                        overallStatus = totalInsightValues == 0 ? "Ruim" : totalInsightValues < 2 ? "Médio" : "Bom";
                    }

                    return new AnalsysDashbaordStatusDto(e.Name, e.Description, overallStatus);
                }).ToArray());
        }

        private static string getCurrency(decimal? amount)
        {
            CultureInfo brazilianCulture = new CultureInfo("pt-BR");
            string formattedAmount = String.Format(brazilianCulture, "{0:C}", amount);

            return formattedAmount;
        }
    }
}
