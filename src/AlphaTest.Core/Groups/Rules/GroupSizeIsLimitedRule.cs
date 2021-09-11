using AlphaTest.Core.Common;
using System.Collections.Generic;

namespace AlphaTest.Core.Groups.Rules
{
    public class GroupSizeIsLimitedRule : IBusinessRule
    {
        public static readonly int GROUP_MAX_SIZE = 100;
        
        private readonly List<Membership> _memberships;

        public GroupSizeIsLimitedRule(List<Membership> memberships)
        {
            _memberships = memberships;
        }
        public string Message => $"Достигнута максимальная численность группы - {GROUP_MAX_SIZE} человек.";

        public bool IsBroken => _memberships.Count == GROUP_MAX_SIZE;
    }
}
