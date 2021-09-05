using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Publishing.Rules;

namespace AlphaTest.Core.Tests.Publishing
{
    public class PublishingProposal: Entity
    {
		private PublishingProposal() { }

		public PublishingProposal(int testID)
        {
			TestID = testID;
			// TBD можно ли использовать DateTime.Now
			SentAt = DateTime.Now;
			Status = ProposalStatus.NEW;
			// ToDo Domain event
		}

		public int ID { get; private set; }

        public int TestID { get; private set; }

		public int AssigneeID { get; private set; }

		public ProposalStatus Status { get; private set; }

        public DateTime SentAt { get; private set; }

		public DateTime AssignedAt { get; private set; }

		public DateTime FinishedAt { get; private set; }

		public string Remark { get; private set; }

		public void Assign(int assigneeID)
        {
			throw new NotImplementedException();
			/*
			AssignedAt = DateTime.Now;
			AssigneeID = assigneeID;
			Status = ProposalStatus.PENDING;
			*/
        }

		public void Approve()
        {
			FinishedAt = DateTime.Now;
			Status = ProposalStatus.APPROVED;
			// ToDo domain event
        }

		public void Decline(string remark)
        {
			CheckRule(new RemarkMustBeProvidedWhenProposalIsDeclinedRule(remark));
			Status = ProposalStatus.DECLINED;
			Remark = remark;
			// ToDo domain event
        }

		
        
    }
}
