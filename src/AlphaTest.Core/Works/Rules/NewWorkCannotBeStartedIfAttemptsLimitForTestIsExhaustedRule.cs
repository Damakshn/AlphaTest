using AlphaTest.Core.Common;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Works.Rules
{
    public class NewWorkCannotBeStartedIfAttemptsLimitForTestIsExhaustedRule : IBusinessRule
    {
        private readonly Test _test;

        private readonly uint _attemptsSpent;

        public NewWorkCannotBeStartedIfAttemptsLimitForTestIsExhaustedRule(Test test, uint attemptsSpent)
        {
            _test = test;
            _attemptsSpent = attemptsSpent;
        }

        public string Message => "Нельзя начать тестирование - количество попыток исчерпано.";

        public bool IsBroken => CheckIfNoAttemptsRemained();

        private bool CheckIfNoAttemptsRemained()
        {
            if (_test.AttemptsLimit is null)
                return false;
            return (uint)_test.AttemptsLimit <= _attemptsSpent;
        }
    }
}
