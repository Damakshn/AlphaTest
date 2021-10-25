using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangePassingScore
{
    public class ChangePassingScoreUseCaseRequest : IUseCaseRequest
    {
        public ChangePassingScoreUseCaseRequest(Guid testID, uint newScore)
        {
            TestID = testID;
            NewScore = newScore;
        }
        public Guid TestID { get; private set; }

        public uint NewScore { get; private set; }
    }
}
