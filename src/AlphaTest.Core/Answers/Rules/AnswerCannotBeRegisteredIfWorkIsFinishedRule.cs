using AlphaTest.Core.Common;
using AlphaTest.Core.Works;

namespace AlphaTest.Core.Answers.Rules
{
    public class AnswerCannotBeRegisteredIfWorkIsFinishedRule : IBusinessRule
    {
        private readonly Work _work;

        public AnswerCannotBeRegisteredIfWorkIsFinishedRule(Work work)
        {
            _work = work;
        }

        public string Message => "Невозможно зарегистрировать ответ - тестирование уже завершилось.";

        public bool IsBroken => _work.IsFinished;
    }
}
