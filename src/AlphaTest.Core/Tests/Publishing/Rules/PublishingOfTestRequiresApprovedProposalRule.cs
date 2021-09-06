using AlphaTest.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTest.Core.Tests.Publishing.Rules
{
    public class PublishingOfTestRequiresApprovedProposalRule : IBusinessRule
    {
        private readonly PublishingProposal _proposal;

        public PublishingOfTestRequiresApprovedProposalRule(PublishingProposal proposal)
        {
            _proposal = proposal;
        }

        public string Message => $"Заявка на публикацию должна быть одобрена, текущий статус заявки - {_proposal.Status}.";

        public bool IsBroken => _proposal.Status != ProposalStatus.APPROVED;
    }
}
