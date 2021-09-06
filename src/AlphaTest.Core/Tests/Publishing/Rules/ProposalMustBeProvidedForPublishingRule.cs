using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Publishing.Rules
{
    public class ProposalMustBeProvidedForPublishingRule : IBusinessRule
    {
        private readonly PublishingProposal _proposal;

        public ProposalMustBeProvidedForPublishingRule(PublishingProposal proposal)
        {
            _proposal = proposal;
        }

        public string Message => "Для публикации теста необходимо предоставить заявку";

        public bool IsBroken => _proposal is null;
    }
}
