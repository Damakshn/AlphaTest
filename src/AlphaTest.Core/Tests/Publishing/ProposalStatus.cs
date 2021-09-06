using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.Publishing
{
    public class ProposalStatus: Enumeration<ProposalStatus>
    {
        private ProposalStatus(int id, string name):base(id, name) { }

        public static readonly ProposalStatus NEW = new(1, "Новая");

        public static readonly ProposalStatus PENDING = new(2, "В работе");

        public static readonly ProposalStatus APPROVED = new(3, "Одобрена");

        public static readonly ProposalStatus DECLINED = new(4, "Отклонена");
    }
}
