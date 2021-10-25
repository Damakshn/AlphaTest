using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.SendPublishingProposal
{
    public class SendPublishingProposalUseCaseRequest : IUseCaseRequest<Guid>
    {
        public SendPublishingProposalUseCaseRequest(Guid testID)
        {
            TestID = testID;
        }

        public Guid TestID { get; private set; }
    }
}
