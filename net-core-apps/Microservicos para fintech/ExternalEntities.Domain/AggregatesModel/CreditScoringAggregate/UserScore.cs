using BrDateTimeUtils;
using ExternalEntities.Domain.Abstractions;
using System;
using System.Collections.Generic;

namespace ExternalEntities.Domain.AggregatesModel.CreditScoringAggregate
{
    public class UserScore : BaseEntity
    {
        public int Score { get; set; }
        public bool FullAnalysisDone { get; set; }
        public List<ScoreFilterHistory> History { get; set; }

        private UserScore() { }
        public UserScore(int score)
        {
            Score = score;
            FullAnalysisDone = true;
        }

        public UserScore(int score, bool fullAnalysisDone)
        {
            Score = score;
            FullAnalysisDone = fullAnalysisDone;
        }

        public void AddHistory(string description)
        {
            History = History ?? new List<ScoreFilterHistory>();
            History.Add(new ScoreFilterHistory(description, DateTime.Now.Brasilia()));
        } 
    }

    public record ScoreFilterHistory(string description, DateTime createdDate);
}
