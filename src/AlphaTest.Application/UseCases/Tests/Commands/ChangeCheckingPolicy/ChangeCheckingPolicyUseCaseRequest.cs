using System;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Tests.TestSettings.Checking;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeCheckingPolicy
{
    public class ChangeCheckingPolicyUseCaseRequest : IUseCaseRequest
    {
        public ChangeCheckingPolicyUseCaseRequest(Guid testID, int policyID)
        {
            TestID = testID;
            NewPolicy = CheckingPolicy.ParseFromID(policyID);
        }

        public Guid TestID { get; private set; }

        public CheckingPolicy NewPolicy { get; private set; }
    }
}
