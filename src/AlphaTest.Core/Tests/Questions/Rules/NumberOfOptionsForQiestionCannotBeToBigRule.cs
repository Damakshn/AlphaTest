using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Common;


namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class NumberOfOptionsForQiestionCannotBeToBigRule : IBusinessRule
    {
        public static readonly int MAX_OPTIONS = 20;

        private readonly IEnumerable<QuestionOption> _options;

        public NumberOfOptionsForQiestionCannotBeToBigRule(IEnumerable<QuestionOption> options)
        {
            _options = options;
        }

        public string Message => $"Число вариантов ответа не может превышать {MAX_OPTIONS}.";

        public bool IsBroken => _options.Count() > MAX_OPTIONS;
    }
}
