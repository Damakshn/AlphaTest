using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Admin.Commands.Proposals.Publishing.AssignProposal
{
    public class AssignProposalUseCaseRequest : IUseCaseRequest
    {
        public AssignProposalUseCaseRequest(Guid proposalID, Guid assigneeID)
        {
            ProposalID = proposalID;
            AssigneeID = assigneeID;
        }

        public Guid ProposalID { get; private set; }

        public Guid AssigneeID { get; private set; }
    }
}
