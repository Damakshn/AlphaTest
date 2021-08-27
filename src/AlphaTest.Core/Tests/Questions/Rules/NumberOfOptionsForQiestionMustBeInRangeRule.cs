using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Common;


namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class NumberOfOptionsForQiestionMustBeInRangeRule : IBusinessRule
    {
        public static readonly int MIN_OPTIONS = 2;
        public static readonly int MAX_OPTIONS = 20;

        private readonly IEnumerable<QuestionOption> _options;

        public NumberOfOptionsForQiestionMustBeInRangeRule(IEnumerable<QuestionOption> options)
        {
            _options = options;
        }

        public string Message => $"Число вариантов ответа должно быть от {MIN_OPTIONS} до {MAX_OPTIONS}.";

        public bool IsBroken => _options.Count() > MAX_OPTIONS || _options.Count() < MIN_OPTIONS;
    }
}
