using System.Collections.Generic;
using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class QuestionOptionsMustBeSpecifiedRule : IBusinessRule
    {   
        private readonly List<(string text, uint number, bool isRight)> _optionsData;
        
        public QuestionOptionsMustBeSpecifiedRule(List<(string text, uint number, bool isRight)> optionsData)
        {
            _optionsData = optionsData;
        }

        public string Message => "Варианты ответа на вопрос должны быть заполнены.";

        public bool IsBroken => _optionsData is null;
    }
}
