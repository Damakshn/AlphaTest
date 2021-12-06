using AlphaTest.Core.Common;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Users;
using System.Linq;

namespace AlphaTest.Core.Examinations.Rules
{
    public class ExaminerMustBeAuthorOrContributorOfTheTestRule : IBusinessRule
    {
        private readonly IAlphaTestUser _examiner;
        private readonly Test _test;

        public ExaminerMustBeAuthorOrContributorOfTheTestRule(IAlphaTestUser examiner, Test test)
        {
            _examiner = examiner;
            _test = test;
        }

        public string Message => "Экзаменатор должен быть автором теста или входить в число составителей.";
        
        public bool IsBroken => 
            _test.AuthorID != _examiner.Id && 
            _test.Contributions.Count(c => c.TeacherID == _examiner.Id) == 0;
    }
}
