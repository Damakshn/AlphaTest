using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Admin.Commands.Proposals.Publishing.DeclineProposal
{
    public class DeclineProposalUseCaseRequest : IUseCaseRequest
    {
        public DeclineProposalUseCaseRequest(Guid proposalID, string remark)
        {
            ProposalID = proposalID;
            Remark = remark;
        }

        public Guid ProposalID { get; private set; }

        public string Remark { get; private set; }
    }
}
