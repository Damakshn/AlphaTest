using AlphaTest.Core.Common;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Answers.Rules
{
    public class AnswerCannotBeRevokedIfNumberOfRetriesIsExhaustedRule : IBusinessRule
    {
        private readonly Test _test;

        private readonly uint _retriesUsed;

        public AnswerCannotBeRevokedIfNumberOfRetriesIsExhaustedRule(Test test, uint retriesUsed)
        {
            _test = test;
            _retriesUsed = retriesUsed;
        }

        public string Message => "Невозможно отозвать ответ - количество попыток исчерпано.";

        public bool IsBroken => 
            _test.RevokePolicy.InfiniteRetriesEnabled
            ? false
            : _test.RevokePolicy.RetriesLimit <= _retriesUsed;
    }
}
