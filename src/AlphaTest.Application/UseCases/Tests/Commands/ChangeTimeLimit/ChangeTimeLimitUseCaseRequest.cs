using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeTimeLimit
{
    public class ChangeTimeLimitUseCaseRequest : IUseCaseRequest
    {
        public ChangeTimeLimitUseCaseRequest(Guid testID, TimeSpan? timeLimit)
        {
            TestID = testID;
            TimeLimit = timeLimit;
        }
        public Guid TestID { get; private set; }

        public TimeSpan? TimeLimit { get; private set; }
    }
}
