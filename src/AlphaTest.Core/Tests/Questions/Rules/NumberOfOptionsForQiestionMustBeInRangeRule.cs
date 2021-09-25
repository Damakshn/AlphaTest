using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Common;


namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class NumberOfOptionsForQiestionMustBeInRangeRule : IBusinessRule
    {
        public static readonly int MIN_OPTIONS = 2;
        public static readonly int MAX_OPTIONS = 20;

        private readonly IEnumerable<(string text, uint number, bool isRight)> _optionsData;

        public NumberOfOptionsForQiestionMustBeInRangeRule(IEnumerable<(string text, uint number, bool isRight)> optionsData)
        {
            _optionsData = optionsData;
        }

        public string Message => $"Число вариантов ответа должно быть от {MIN_OPTIONS} до {MAX_OPTIONS}.";

        public bool IsBroken => _optionsData.Count() > MAX_OPTIONS || _optionsData.Count() < MIN_OPTIONS;
    }
}
