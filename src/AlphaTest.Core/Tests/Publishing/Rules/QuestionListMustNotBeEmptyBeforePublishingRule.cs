using System.Collections.Generic;
using AlphaTest.Core.Common;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Tests.Publishing.Rules
{
    public class QuestionListMustNotBeEmptyBeforePublishingRule : IBusinessRule
    {
        private readonly List<Question> _allQuestionsInTest;

        public QuestionListMustNotBeEmptyBeforePublishingRule(List<Question> allQuestionsInTest)
        {
            _allQuestionsInTest = allQuestionsInTest;
        }

        public string Message => $"В тесте нет вопросов, поэтому публикация невозможна.";

        public bool IsBroken => _allQuestionsInTest.Count == 0;
    }
}
