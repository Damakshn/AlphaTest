using AlphaTest.Core.Common;
using AlphaTest.Core.Works;

namespace AlphaTest.Core.Answers.Rules
{
    public class AnswerCannotBeRevokedIfWorkIsFinishedRule : IBusinessRule
    {
        private readonly Work _work;

        public AnswerCannotBeRevokedIfWorkIsFinishedRule(Work work)
        {
            _work = work;
        }

        public string Message => "Невозможно отозвать ответ на вопрос - время тестирования истекло.";

        public bool IsBroken => _work.IsFinished;
    }
}
