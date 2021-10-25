using System;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Tests.TestSettings.Checking;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeWorkCheckingMethod
{
    public class ChangeWorkCheckingMethodUseCaseRequest : IUseCaseRequest
    {
        public ChangeWorkCheckingMethodUseCaseRequest(Guid testID, int workCheckingMethodID)
        {
            TestID = testID;
            NewWorkCheckingMethod = WorkCheckingMethod.ParseFromID(workCheckingMethodID);
        }
        public Guid TestID { get; private set; }

        public WorkCheckingMethod NewWorkCheckingMethod { get; set; }
    }
}
