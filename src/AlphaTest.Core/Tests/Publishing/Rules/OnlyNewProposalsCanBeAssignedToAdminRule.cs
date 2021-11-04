using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Publishing.Rules
{
    public class OnlyNewProposalsCanBeAssignedToAdminRule : IBusinessRule
    {
        private readonly PublishingProposal _proposal;

        public OnlyNewProposalsCanBeAssignedToAdminRule(PublishingProposal proposal)
        {
            _proposal = proposal;
        }

        public string Message => $"Только новая заявка может быть взята в работу, текущий статус заявки - {_proposal.Status.Name}";

        public bool IsBroken => _proposal.Status != ProposalStatus.NEW;
    }
}
