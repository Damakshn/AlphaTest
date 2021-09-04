using System.Collections.Generic;
using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class QuestionOptionsMustBeSpecifiedRule : IBusinessRule
    {
        private readonly List<QuestionOption> _options;

        public QuestionOptionsMustBeSpecifiedRule(List<QuestionOption> options)
        {
            _options = options;
        }

        public string Message => "Варианты ответа на вопрос должны быть заполнены.";

        public bool IsBroken => _options is null;
    }
}
