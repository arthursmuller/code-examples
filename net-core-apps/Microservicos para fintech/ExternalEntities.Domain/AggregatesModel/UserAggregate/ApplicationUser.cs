using BrDateTimeUtils;
using ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate;
using ExternalEntities.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExternalEntities.Domain.AggregatesModel.UserAggregate
{
    public class ApplicationUser : BaseEntity, IAggregateRoot
    {
        public string Cpf { get; set; }
        public DateTime? BirthDate { get; set; }
        public Address Address { get; set; }
        public IReadOnlyCollection<UserScore> Scores => _scores;
        private List<UserScore> _scores;
        public IReadOnlyCollection<ExternalQuodDataRecord> QuodDataRecords => _quodDataRecords;
        private List<ExternalQuodDataRecord> _quodDataRecords;
        public decimal? TotalDelinquencyAmount { get; set; }
        public decimal? TotalCurrentDebitAmount { get; set; }
        public decimal? TotalCurrentDebitDelinquentAmount { get; set; }
        public decimal? TotalOverdrawnAmount { get; set; }
        public decimal? TotalExternalCreditAvailable { get; set; }
        public int? TotalFinancialOperations { get; set; }
        public bool IsDenied => isDenied(GetCurrentScore());

        private ApplicationUser() { }
        public ApplicationUser(int id, string cpf) : base(id) => Cpf = cpf;
   
        public bool ShouldMakeFullAnalysis()
        {
            var score = GetCurrentScore();
            var isFullAnalysisDone = score?.FullAnalysisDone ?? false;
            return checkScoreDateRangeIsValid() || !isFullAnalysisDone;
        }
        public bool ShouldMakeSimulation(int? maxHours = 15 * 24)
        {
            return checkScoreDateRangeIsValid();
        }
        public void AddQuodData(ExternalQuodData data, bool applyFilters)
        {
            addQuodRecord(data);
            addScore(data?.Score?.Score ?? 0);
            var score = GetCurrentScore();
            if (applyFilters) applyQuodFilters(score, data);
        }
        public void AddFinancialAnalysisData(
            decimal? totalDelinquencyAmoun,
            decimal? totalCurrentDebitAmount,
            decimal? totalCurrentDebitDelinquentAmount,
            decimal? totalOverdrawnAmount,
            decimal? totalCreditToBeAvailable,
            int? totalFinancialOperations, 
            bool? applyFilters = false) 
        {
            TotalDelinquencyAmount = totalDelinquencyAmoun;
            TotalCurrentDebitAmount = totalCurrentDebitAmount;
            TotalCurrentDebitDelinquentAmount = totalCurrentDebitDelinquentAmount;
            TotalExternalCreditAvailable = totalCreditToBeAvailable;
            TotalFinancialOperations = totalFinancialOperations;
            TotalOverdrawnAmount = totalOverdrawnAmount;

            if(applyFilters ?? false)
            {
                var score = GetCurrentScore();
                filterScrFinancialAnalysis(score);
            }
        }
        public UserScore GetCurrentScore() => (_scores ?? new List<UserScore>())?.OrderByDescending(e => e?.CreatedDate)?.FirstOrDefault();
        public ExternalQuodData GetCurrentQuodData() => (_quodDataRecords ?? new List<ExternalQuodDataRecord>())?.OrderByDescending(e => e?.CreatedDate)?.FirstOrDefault()?.ExternalQuodData;
        public int GetScore() => (_scores ?? new List<UserScore>())?.OrderByDescending(e => e?.CreatedDate)?.FirstOrDefault()?.Score ?? 0;
        public void AddSimulation(ExternalQuodData data)
        {
            addQuodRecord(data);
            addSimulationScore(data?.Score?.Score ?? 0);
        }
        private void addScore(int score)
        {
            _scores = _scores ?? new List<UserScore>();
            var newScore = new UserScore(score);
            filterMinimumScore(newScore);
            _scores.Add(newScore);
        }
        private void addSimulationScore(int score)
        {
            _scores = _scores ?? new List<UserScore>();
            var newScore = new UserScore(score, false);
            filterMinimumScore(newScore);
            _scores.Add(newScore);
        }
        private void addQuodRecord(ExternalQuodData data)
        {
            _quodDataRecords = _quodDataRecords ?? new List<ExternalQuodDataRecord>();
            _quodDataRecords.Add(new ExternalQuodDataRecord(data));
            BirthDate = data.BirthDate;
            addAddress(data);
        }
        private void addAddress(ExternalQuodData data)
        {
            int? parsedValue = null;
            int parsedValueHelper;

            if (int.TryParse(data.CurrentAddress?.Number, out parsedValueHelper))
                parsedValue = parsedValueHelper;

            Address = new Address(
                data.CurrentAddress?.City, 
                data.CurrentAddress?.PostalCode, 
                data.CurrentAddress?.State,
                data.CurrentAddress?.Street, "Brazil",
                parsedValue, 
                data.CurrentAddress?.Complement, 
                data.CurrentAddress?.Neighborhood, "");
        }
        private void applyQuodFilters(UserScore score, ExternalQuodData data)
        {
            filterPersonalInfo(score, data);
            filterQuodFinancialAnalysis(score, data);
            filterQuodScore(score);
        }
        private void filterPersonalInfo(UserScore score, ExternalQuodData data)
        {
            if (!isDenied(score))
            {
                if (BirthDate?.GetAge() > 67 || BirthDate?.GetAge() < 18)
                    reject(score, nameof(BirthDate));

                filterMinimumScore(score);
            }
        }
        private void filterQuodFinancialAnalysis(UserScore score, ExternalQuodData data)
        {
            if (!isDenied(score))
            {
                if (data?.Negative?.PendenciesControlCred > 0)
                    reject(score, "PendenciesControlCred");

                if (data?.Protests?.Count > 0)
                    reject(score, $"Protestos: {data?.Protests?.Count}");

                filterMinimumScore(score);
            }
        }
        private void filterScrFinancialAnalysis(UserScore score)
        {
            if (!isDenied(score))
            {
                if ((TotalDelinquencyAmount ?? 0) > 0)
                    reject(score, nameof(TotalDelinquencyAmount));
                if ((TotalCurrentDebitDelinquentAmount ?? 0) > 0)
                    reject(score, nameof(TotalCurrentDebitDelinquentAmount));
                if (TotalFinancialOperations <= 1)
                    reduceScore(score, null, 5, nameof(TotalFinancialOperations));
                
                filterMinimumScore(score);
            }
        }
        private void filterQuodScore(UserScore score)
        {
            if(!isDenied(score))
            {
                var commitmentScore = QuodDataRecords?.LastOrDefault()?.ExternalQuodData?.Score?.PaymentCommitmentScore ?? 0;
                var profileScore = QuodDataRecords?.LastOrDefault()?.ExternalQuodData?.Score?.ProfileScore ?? 0;

                if (commitmentScore <= 69)
                {
                    reject(score, nameof(commitmentScore));
                }
                else if (commitmentScore > 90) 
                {
                    increaseScore(score, null, 10, nameof(commitmentScore));
                }

                if (profileScore <= 47)
                {
                    reject(score, nameof(profileScore));
                }
                else if(profileScore >= 90)
                {
                    increaseScore(score, null, 15, nameof(profileScore));
                }
                else if(profileScore > 80)
                {
                    increaseScore(score, null, 5, nameof(profileScore));
                }

                filterMinimumScore(score);
            }
        }
        private void filterMinimumScore(UserScore score)
        {
            const int kickScore = 700;

            if(!isDenied(score) && score.Score < kickScore)
                reject(score, $"(score < {kickScore})");
        }
        private bool reduceScore(UserScore score, int? maxScore, int weight, string description)
        {
            var reduced = false;
            if(!isDenied(score))
            {
                var message = $"Filter: {description.ToUpper()} - S:{score.Score} - W:{weight} = " + "{0}";
                if (maxScore is null)
                {
                    score.Score -= weight;
                    reduced = true;
                    score.AddHistory(string.Format(message, score.Score));
                }
                else if (score.Score <= maxScore)
                {
                    score.Score -= weight;
                    reduced = true;
                    score.AddHistory(string.Format(message, score.Score));
                }
            }

            return reduced;
        }
        private bool increaseScore(UserScore score, int? maxScore, int weight, string description)
        {
            var increased = false;
            if(!isDenied(score))
            {
                var message = $"Filter: {description.ToUpper()} - S:{score.Score} + W:{weight} = " + "{0}";
                if (maxScore is null)
                {
                    score.Score += weight;
                    increased = true;
                    score.AddHistory(string.Format(message, score.Score));
                }
                else if (score.Score <= maxScore)
                {
                    score.Score += weight;
                    increased = true;
                    score.AddHistory(string.Format(message, score.Score));
                }

                if(score.Score > 1000)
                {
                    var remaining = score.Score - 1000;
                    score.Score -= remaining;
                }
            }

            return increased;
        }
        private bool isDenied(UserScore score) => score?.Score == -1;
        private int reject(UserScore score, string description)
        {
            score.AddHistory($"Filter: {description.ToUpper()} - S:{score.Score} - W:{-1} = -1");
            score.Score = -1;
            return -1;
        }
        private bool checkScoreDateRangeIsValid(int? maxHours = 24 * 15)
        {
            var score = GetCurrentScore();
            var lastScoreCheckDate = score?.CreatedDate;
            return (DateTime.Now.Brasilia() - lastScoreCheckDate)?.TotalHours > maxHours || (_scores?.Count == 0) || _scores is null;
        }
        /*       public decimal PresumedIncome { get; set; }
       public decimal PresumedMinmumAllowedExpense { get; set; }
       public decimal PendingCreditLineAmount { get; set; }
       public bool HasCompany { get; set; }
       public string ScoreStatus { get; set; }

       private int filterPresumedIncome(int score) 
       {
           if (!HasCompany && GetCurrentScore().Score < 870)
           {
               PresumedMinmumAllowedExpense = 300;
               score = PresumedMinmumAllowedExpense <= 90 ? - 1 : score;

               if (PresumedMinmumAllowedExpense <= 100)
               {
                   score = reduceScore(score, 690, score+1);
                   score = reduceScore(score, 740, score+1);
                   score = reduceScore(score, 800, 120);
               }
               else if (PresumedMinmumAllowedExpense <= 200)
               {
                   score = reduceScore(score, 690, 120);
                   score = reduceScore(score, 740, 95);
                   score = reduceScore(score, 800, 75);
               }
               else if(PresumedMinmumAllowedExpense <= 300)
               {
                   score = reduceScore(score, 690, 90);
                   score = reduceScore(score, 740, 75);
                   score = reduceScore(score, 800, 65);
               }
               else if(PresumedMinmumAllowedExpense <= 400)
               {
                   score = reduceScore(score, 690, score + 1);
                   score = reduceScore(score, 740, score + 1);
                   score = reduceScore(score, 800, 120);
               }
               else if(PresumedMinmumAllowedExpense <= 500)
               {
                   score = reduceScore(score, 690, score + 1);
                   score = reduceScore(score, 740, score + 1);
                   score = reduceScore(score, 800, 120);
               }
               else if(PresumedMinmumAllowedExpense <= 600)
               {
                   score = reduceScore(score, 690, 210);
                   score = reduceScore(score, 690, 210);
                   score = reduceScore(score, 690, 210);
               }
               else if(PresumedMinmumAllowedExpense >= 700)
               {
                   score = reduceScore(score, 690, 30);
                   score = reduceScore(score, 690, 210);
                   score = reduceScore(score, 690, 210);
               }

               if (PresumedIncome > 5.000m)
                   score = incrementScore(score, 800, 20);
               else if(PresumedIncome > 20.000m)
                   score = incrementScore(score, 800, 35);
           }

           return score;
       }
*/
        /*    
            }*/
    }
}
