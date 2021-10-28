using System;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeScoreDistribution
{
    public class ChangeScoreDistributionUseCaseRequest : IUseCaseRequest
    {
        public ChangeScoreDistributionUseCaseRequest(Guid testID, int scoreDistributionMethodID, int? score = null)
        {
            TestID = testID;
            ScoreDistributionMethod = ScoreDistributionMethod.ParseFromID(scoreDistributionMethodID);
            Score = score is not null ? new QuestionScore((int)score) : null;
        }

        public Guid TestID { get; private set; }

        public ScoreDistributionMethod ScoreDistributionMethod { get; private set; }

        public QuestionScore Score { get; private set; }
    }
}
