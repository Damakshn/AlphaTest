using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Rules
{
    public class OnlyPublishedTestsCanBeReplicatedRule : IBusinessRule
    {
        private readonly Test _test;

        public OnlyPublishedTestsCanBeReplicatedRule(Test test)
        {
            _test = test;
        }

        public string Message => "Создание новой версии допустимо только для опубликованных тестов.";

        public bool IsBroken => _test.Status != TestStatus.Published;
    }
}
