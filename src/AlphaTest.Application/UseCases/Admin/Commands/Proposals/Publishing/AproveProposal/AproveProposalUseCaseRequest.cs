using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Admin.Commands.Proposals.Publishing.AproveProposal
{
    public class AproveProposalUseCaseRequest : IUseCaseRequest
    {
        public AproveProposalUseCaseRequest(Guid proposalID)
        {
            ProposalID = proposalID;
        }

        public Guid ProposalID { get; private set; }
    }
}
