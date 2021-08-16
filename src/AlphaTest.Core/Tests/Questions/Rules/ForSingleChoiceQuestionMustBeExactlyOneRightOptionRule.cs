using AlphaTest.Core.Common;
using System.Collections.Generic;
using System.Linq;

namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule : IBusinessRule
    {
        private readonly IEnumerable<QuestionOption> _options;

        public ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule(IEnumerable<QuestionOption> options)
        {
            _options = options;
        }

        public string Message => "Для вопроса с выбором одного варианта ответа только один вариант может быть верным.";

        public bool IsBroken => _options.Count(o => o.IsRight) != 1;
    }
}
