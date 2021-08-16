using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Rules
{
    public class AttemptsLimitForTestMustBeInRangeRule: IBusinessRule
    {
        public static readonly uint MIN_ATTEMPTS_PER_TEST = 1;
        public static readonly uint MAX_ATTEMPTS_PER_TEST = 100;

        private readonly uint? _attemptsPerTest;

        public AttemptsLimitForTestMustBeInRangeRule(uint? attemptsPerTest)
        {
            _attemptsPerTest = attemptsPerTest;
        }

        public string Message => $"Количество попыток сдачи теста должно быть в интервале от {MIN_ATTEMPTS_PER_TEST} до {MAX_ATTEMPTS_PER_TEST}.";

        public bool IsBroken => 
            (_attemptsPerTest is not null) && 
            (_attemptsPerTest > MAX_ATTEMPTS_PER_TEST || _attemptsPerTest < MIN_ATTEMPTS_PER_TEST);
    }
}
