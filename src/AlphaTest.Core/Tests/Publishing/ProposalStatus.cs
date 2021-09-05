using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.Publishing
{
    public class ProposalStatus: Enumeration<ProposalStatus>
    {
        private ProposalStatus(int id, string name):base(id, name) { }

        public static ProposalStatus NEW = new(1, "Новая");

        public static ProposalStatus PENDING = new(2, "В работе");

        public static ProposalStatus APPROVED = new(3, "Одобрена");

        public static ProposalStatus DECLINED = new(4, "Отклонена");
    }
}
