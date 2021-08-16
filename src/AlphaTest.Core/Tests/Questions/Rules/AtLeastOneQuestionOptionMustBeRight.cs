using System.Linq;
using System.Collections.Generic;
using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class AtLeastOneQuestionOptionMustBeRight : IBusinessRule
    {
        private IEnumerable<QuestionOption> _options;

        public AtLeastOneQuestionOptionMustBeRight(IEnumerable<QuestionOption> options)
        {
            _options = options;
        }

        public string Message => "Хотя бы один вариант ответа должен быть верным.";

        public bool IsBroken => _options.Any(o => o.IsRight) == false;
    }
}
