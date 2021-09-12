using System.Linq;
using AlphaTest.Core.Common;
using AlphaTest.Core.Groups;
using System.Collections.Generic;

namespace AlphaTest.Core.Examinations.Rules
{
    public class DisbandedOrInactiveGroupsCannotParticipateExamRule : IBusinessRule
    {
        private readonly IEnumerable<Group> _candidateGroups;

        private readonly Group _candidateGroup;

        public DisbandedOrInactiveGroupsCannotParticipateExamRule(IEnumerable<Group> candidates)
        {
            _candidateGroups = candidates;
        }

        public DisbandedOrInactiveGroupsCannotParticipateExamRule(Group candidateGroup)
        {
            _candidateGroup = candidateGroup;
        }

        // ToDo хорошо бы указать, какая из групп не прошла проверку
        public string Message => "Расформированные и неактивные группы не могут принимать участие в экзамене.";

        // ToDo predicate
        public bool IsBroken => 
            _candidateGroup is not null 
            ? _candidateGroup.IsDisbanded || _candidateGroup.IsGone
            : _candidateGroups.Any(g => g.IsDisbanded || g.IsGone);
    }
}
