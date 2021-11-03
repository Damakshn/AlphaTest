using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.NewEdition
{
    public class CreateNewEditionOfTestUseCaseRequest : IUseCaseRequest<Guid>
    {
        public CreateNewEditionOfTestUseCaseRequest(Guid testID)
        {
            TestID = testID;
        }

        public Guid TestID { get; private set; }
    }
}
