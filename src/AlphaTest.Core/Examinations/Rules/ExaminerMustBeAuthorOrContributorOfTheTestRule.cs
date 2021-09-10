using AlphaTest.Core.Common;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Examinations.Rules
{
    public class ExaminerMustBeAuthorOrContributorOfTheTestRule : IBusinessRule
    {
        private readonly User _examiner;
        private readonly Test _test;

        public ExaminerMustBeAuthorOrContributorOfTheTestRule(User examiner, Test test)
        {
            _examiner = examiner;
            _test = test;
        }

        public string Message => "Экзаменатор должен быть автором теста или входить в число составителей.";

        // ToDo зашить список составителей внутрь теста
        public bool IsBroken => false;
    }
}
