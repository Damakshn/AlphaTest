using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Examinations.Rules
{
    public class ExaminerMustBeTeacherRule : IBusinessRule
    {
        private readonly User _examiner;

        public ExaminerMustBeTeacherRule(User examiner)
        {
            _examiner = examiner;
        }

        public string Message => "Экзаменатор должен быть преподавателем.";

        public bool IsBroken => _examiner.IsInRole(UserRole.TEACHER) == false;
    }
}
