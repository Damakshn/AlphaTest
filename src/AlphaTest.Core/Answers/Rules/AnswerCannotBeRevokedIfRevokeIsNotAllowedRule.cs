using AlphaTest.Core.Common;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.Answers.Rules
{
    public class AnswerCannotBeRevokedIfRevokeIsNotAllowedRule : IBusinessRule
    {
        private readonly Test _test;

        public AnswerCannotBeRevokedIfRevokeIsNotAllowedRule(Test test)
        {
            _test = test;
        }

        public string Message => "Операция невозможна - отзыв ответов запрещён.";

        public bool IsBroken => _test.RevokePolicy.RevokeEnabled == false;
    }
}
