using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Examinations.Rules
{
    public class ExaminerMustBeTeacherRule : IBusinessRule
    {
        private readonly IAlphaTestUser _examiner;

        public ExaminerMustBeTeacherRule(IAlphaTestUser examiner)
        {
            _examiner = examiner;
        }

        public string Message => "Экзаменатор должен быть преподавателем.";

        public bool IsBroken => _examiner.IsTeacher == false;
    }
}
