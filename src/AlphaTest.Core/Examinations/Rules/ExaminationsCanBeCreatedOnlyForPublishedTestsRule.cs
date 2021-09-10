using AlphaTest.Core.Common;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Examinations.Rules
{
    public class ExaminationsCanBeCreatedOnlyForPublishedTestsRule : IBusinessRule
    {
        private readonly Test _test;

        public ExaminationsCanBeCreatedOnlyForPublishedTestsRule(Test test)
        {
            _test = test;
        }

        public string Message => "Экзамен можно проводить только по опубликованному тесту";

        public bool IsBroken => _test.Status != TestStatus.Published;
    }
}
