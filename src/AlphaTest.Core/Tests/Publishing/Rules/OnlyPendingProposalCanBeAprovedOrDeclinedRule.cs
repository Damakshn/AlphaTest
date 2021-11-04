using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Publishing.Rules
{
    public class OnlyPendingProposalCanBeAprovedOrDeclinedRule : IBusinessRule
    {
        private readonly PublishingProposal _proposal;

        public OnlyPendingProposalCanBeAprovedOrDeclinedRule(PublishingProposal proposal)
        {
            _proposal = proposal;
        }

        public string Message => $"Заявка может быть одобрена или отклонена только после назначения исполнителя.";

        public bool IsBroken => _proposal.Status != ProposalStatus.PENDING;
    }
}
