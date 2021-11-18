using System.Linq;
using AlphaTest.Core.Users;
using AlphaTest.Core.Common;


namespace AlphaTest.Core.Groups.Rules
{
    public class StudentCanBeAddedToGroupOnlyOnceRule : IBusinessRule
    {
        private readonly Group _group;

        private readonly IAlphaTestUser _studentToAdd;

        public StudentCanBeAddedToGroupOnlyOnceRule(Group group, IAlphaTestUser studentToAdd)
        {
            _group = group;
            _studentToAdd = studentToAdd;
        }

        public string Message => $"Студент {_studentToAdd.LastName} {_studentToAdd.FirstName} {_studentToAdd.MiddleName} уже состоит в группе {_group.Name}";

        public bool IsBroken => _group.Memberships.Any(m => m.StudentID == _studentToAdd.ID);
    }
}
