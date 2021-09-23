using AlphaTest.Core.Common;
using System.Collections.Generic;
using System.Linq;

namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule : IBusinessRule
    {
        private readonly IEnumerable<(string text, uint number, bool isRight)> _optionsData;

        public ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule(IEnumerable<(string text, uint number, bool isRight)> optionsData)
        {
            _optionsData = optionsData;
        }

        public string Message => "Для вопроса с выбором одного варианта ответа только один вариант может быть верным.";

        public bool IsBroken => _optionsData.Count(o => o.isRight) != 1;
    }
}
