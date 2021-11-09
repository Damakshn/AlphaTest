using AlphaTest.Core.Common;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Answers.Rules
{
    public class AnswerCannotBeRegisteredIfNumberOfRetriesIsExhaustedRule : IBusinessRule
    {
        private readonly Test _test;

        private readonly uint _answersAccepted;

        public AnswerCannotBeRegisteredIfNumberOfRetriesIsExhaustedRule(Test test, uint answersAccepted)
        {
            _test = test;
            _answersAccepted = answersAccepted;
        }

        public string Message => "Невозможно зарегистрировать ответ на вопрос - количество попыток исчерпано.";

        public bool IsBroken => 
            _test.RevokePolicy.InfiniteRetriesEnabled
            ? false
            : _test.RevokePolicy.RetriesLimit + 1 <= _answersAccepted;
    }
}
