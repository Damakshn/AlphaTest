using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Rules
{
    public class NonDraftTestCannotBeEditedRule : IBusinessRule
    {
        private readonly Test _test;

        public NonDraftTestCannotBeEditedRule(Test test)
        {
            _test = test;
        }

        public string Message => $"Только тесты в статусе {TestStatus.Draft} могут быть изменены. Текущий статус - \"{_test.Status}\"";

        public bool IsBroken => _test.Status != TestStatus.Draft;
    }
}
