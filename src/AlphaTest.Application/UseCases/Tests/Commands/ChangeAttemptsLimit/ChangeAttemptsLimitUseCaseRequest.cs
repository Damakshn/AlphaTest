using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeAttemptsLimit
{
    public class ChangeAttemptsLimitUseCaseRequest : IUseCaseRequest
    {
        public ChangeAttemptsLimitUseCaseRequest(Guid testID, uint? attemptsLimit)
        {
            TestID = testID;
            AttemptsLimit = attemptsLimit;
        }

        public Guid TestID { get; private set; }

        public uint? AttemptsLimit { get; private set; }
    }
}
