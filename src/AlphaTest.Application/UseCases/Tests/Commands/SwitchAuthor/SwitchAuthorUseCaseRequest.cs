using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.SwitchAuthor
{
    public class SwitchAuthorUseCaseRequest : IUseCaseRequest
    {
        public SwitchAuthorUseCaseRequest(Guid testID, Guid newAuthorID)
        {
            TestID = testID;
            NewAuthorID = newAuthorID;
        }

        public Guid TestID { get; private set; }

        public Guid NewAuthorID { get; private set; }
    }
}
