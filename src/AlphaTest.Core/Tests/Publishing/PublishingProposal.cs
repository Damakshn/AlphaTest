using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Publishing.Rules;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Tests.Publishing
{
    public class PublishingProposal: Entity
    {
		private PublishingProposal() { }

		internal PublishingProposal(Guid testID)
        {
			TestID = testID;
			// TBD можно ли использовать DateTime.Now
			SentAt = DateTime.Now;
			Status = ProposalStatus.NEW;
			ID = Guid.NewGuid();
			AssignedAt = null;
			FinishedAt = null;
			// ToDo Domain event
		}

		public Guid ID { get; private set; }

        public Guid TestID { get; private set; }

		public Guid? AssigneeID { get; private set; }

		public ProposalStatus Status { get; private set; }

        public DateTime SentAt { get; private set; }

		public DateTime? AssignedAt { get; private set; }

		public DateTime? FinishedAt { get; private set; }

		public string Remark { get; private set; }

		public void AssignTo(IAlphaTestUser admin)
        {
			CheckRule(new ProposalCanBeAssignedOnlyToAdminUsersRule(admin));
			CheckRule(new OnlyNewProposalsCanBeAssignedToAdminRule(this));
			AssignedAt = DateTime.Now;
			AssigneeID = admin.Id;
			Status = ProposalStatus.PENDING;
        }

		public void Approve()
        {
			// todo unit test
			CheckRule(new OnlyPendingProposalCanBeAprovedOrDeclinedRule(this));
			FinishedAt = DateTime.Now;
			Status = ProposalStatus.APPROVED;
			// ToDo domain event
        }

		public void Decline(string remark)
        {
			// todo unit test
			CheckRule(new OnlyPendingProposalCanBeAprovedOrDeclinedRule(this));
			CheckRule(new RemarkMustBeProvidedWhenProposalIsDeclinedRule(remark));
			Status = ProposalStatus.DECLINED;
			Remark = remark;
			// ToDo domain event
        }

		
        
    }
}
