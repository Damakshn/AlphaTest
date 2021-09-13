using AlphaTest.Core.Common;
using AlphaTest.Core.Groups;
using System.Collections.Generic;
using System.Linq;

namespace AlphaTest.Core.Examinations.Rules
{
    public class NonParticipatingGroupsCannotBeExcludedFromExamRule : IBusinessRule
    {
        private readonly Group _group;

        private readonly IEnumerable<Group> _groups;

        private readonly Examination _exam;

        public NonParticipatingGroupsCannotBeExcludedFromExamRule(Group group, Examination exam)
        {
            _group = group;
            _exam = exam;
        }

        public NonParticipatingGroupsCannotBeExcludedFromExamRule(IEnumerable<Group> groups, Examination exam)
        {
            _groups = groups;
            _exam = exam;
            
        }

        public string Message => "Нельзя исключить из списка группу, не участвующую в экзамене.";

        public bool IsBroken =>
            _group is not null
            ? _exam.Participations.Where(p => p.GroupID == _group.ID).FirstOrDefault() is null
            : _groups.Any(g => _exam.Participations.Where(p => p.GroupID == g.ID).FirstOrDefault() is null);
    }
}
