using System;
using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Rules
{
    public class TimeLimitMustBeInRangeRule : IBusinessRule
    {
        public static readonly TimeSpan MIN_LIMIT = new(0, 10, 0);
        public static readonly TimeSpan MAX_LIMIT = new(23, 59, 0);

        private readonly TimeSpan? _timeLimit;

        public TimeLimitMustBeInRangeRule(TimeSpan? limit)
        {
            _timeLimit = limit;
        }

        public string Message => $"Лимит времени на тест должен быть в интервале от {MIN_LIMIT} до {MAX_LIMIT}.";

        public bool IsBroken => (_timeLimit is not null) && (_timeLimit < MAX_LIMIT || _timeLimit > MAX_LIMIT);
    }
}
