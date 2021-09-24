using System.Linq;
using System.Collections.Generic;
using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class AtLeastOneQuestionOptionMustBeRightRule : IBusinessRule
    {
        private IEnumerable<(string text, uint number, bool isRight)> _optionsData;

        public AtLeastOneQuestionOptionMustBeRightRule(IEnumerable<(string text, uint number, bool isRight)> optionsData)
        {
            _optionsData = optionsData;
        }

        public string Message => "Хотя бы один вариант ответа должен быть верным.";

        public bool IsBroken => _optionsData.Any(o => o.isRight) == false;
    }
}
