using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Questions.Rules
{
    public class QuestionScoreMustBeSpecifiedRule : IBusinessRule
    {
        private readonly QuestionScore _score;

        public QuestionScoreMustBeSpecifiedRule(QuestionScore score)
        {
            _score = score;
        }
        public string Message => "При создании вопроса нужно указать балл.";

        public bool IsBroken => _score is null;
    }
}
