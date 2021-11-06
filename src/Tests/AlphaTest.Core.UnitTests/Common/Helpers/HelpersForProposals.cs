using System;
using System.Reflection;
using AlphaTest.Core.Tests.Publishing;

namespace AlphaTest.Core.UnitTests.Common.Helpers
{
    public static class HelpersForProposals
    {
        public static void SetProposalStatus(PublishingProposal proposal, ProposalStatus newStatus)
        {
            if (proposal is null)
                throw new ArgumentNullException(nameof(proposal));
            var statusProperty = "Status";
            var proposalStatusProperty = proposal.GetType().GetProperty(statusProperty, BindingFlags.Public | BindingFlags.Instance);
            if (proposalStatusProperty is null)
                throw new InvalidOperationException($"Поле {statusProperty} не найдено в типе {proposal.GetType()}");
            proposalStatusProperty.SetValue(proposal, newStatus);
        }
    }
}
