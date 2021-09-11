using AlphaTest.Core.Common;

namespace AlphaTest.Core.Groups.Rules
{
    public class DisbandedGroupCannotBeModifiedRule : IBusinessRule
    {
        private readonly Group _group;

        public DisbandedGroupCannotBeModifiedRule(Group group)
        {
            _group = group;
        }
        public string Message => "Операция запрещена, так как данная группа расформирована.";

        public bool IsBroken => _group.IsDisbanded;
    }
}
