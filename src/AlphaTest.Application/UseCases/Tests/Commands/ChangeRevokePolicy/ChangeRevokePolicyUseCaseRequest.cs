using System;
using AlphaTest.Core.Tests.TestSettings.TestFlow;
using AlphaTest.Application.UseCases.Common;


namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeRevokePolicy
{
    public class ChangeRevokePolicyUseCaseRequest : IUseCaseRequest
    {
        public ChangeRevokePolicyUseCaseRequest(Guid testID, bool revokeEnabled, uint retriesLimit, bool infiniteRetriesEnabled)
        {
            TestID = testID;
            RevokePolicy = new(revokeEnabled, retriesLimit, infiniteRetriesEnabled);
        }

        public Guid TestID { get; private set; }

        public RevokePolicy RevokePolicy { get; private set; }


    }
}
