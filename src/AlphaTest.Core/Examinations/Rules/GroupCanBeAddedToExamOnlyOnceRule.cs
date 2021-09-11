using AlphaTest.Core.Common;
using AlphaTest.Core.Groups;
using System.Collections.Generic;
using System.Linq;


namespace AlphaTest.Core.Examinations.Rules
{
    public class GroupCanBeAddedToExamOnlyOnceRule : IBusinessRule
    {
        private readonly Group _group;

        private readonly IEnumerable<Group> _groups;

        private readonly Examination _exam;

        public GroupCanBeAddedToExamOnlyOnceRule(Group group, Examination exam)
        {
            _group = group;
            _exam = exam;
        }

        public GroupCanBeAddedToExamOnlyOnceRule(IEnumerable<Group> groups, Examination exam)
        {
            _groups = groups;
            _exam = exam;
        }

        // ToDo более подробное сообщение
        public string Message => "Группа может быть включена в список на экзамен только 1 раз.";

        public bool IsBroken => 
            _group is not null
            ? _exam.Participations.Where(p => p.GroupID == _group.ID).FirstOrDefault() is not null
            : _exam.Participations.Any(p => _groups.Where(g => g.ID == p.GroupID).FirstOrDefault() is not null);
    }
}
