using AlphaTest.Core.Common;

namespace AlphaTest.Core.Groups.Rules
{
    public class InactiveGroupCannotBeModifiedRule : IBusinessRule
    {
        private readonly Group _group;

        public InactiveGroupCannotBeModifiedRule(Group group)
        {
            _group = group;
        }

        public string Message => "Операция запрещена, так как данная группа неактивна.";

        public bool IsBroken => _group.IsGone;
    }
}
