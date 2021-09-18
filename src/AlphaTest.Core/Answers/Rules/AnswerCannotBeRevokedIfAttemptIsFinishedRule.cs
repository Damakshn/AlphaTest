using AlphaTest.Core.Common;
using AlphaTest.Core.Attempts;

namespace AlphaTest.Core.Answers.Rules
{
    public class AnswerCannotBeRevokedIfAttemptIsFinishedRule : IBusinessRule
    {
        private readonly Attempt _attempt;

        public AnswerCannotBeRevokedIfAttemptIsFinishedRule(Attempt attempt)
        {
            _attempt = attempt;
        }

        public string Message => "Невозможно отозвать ответ на вопрос - время тестирования истекло.";

        public bool IsBroken => _attempt.IsFinished;
    }
}
