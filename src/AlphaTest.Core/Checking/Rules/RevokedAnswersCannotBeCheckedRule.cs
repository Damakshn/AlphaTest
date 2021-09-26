using AlphaTest.Core.Answers;
using AlphaTest.Core.Common;

namespace AlphaTest.Core.Checking.Rules
{
    public class RevokedAnswersCannotBeCheckedRule : IBusinessRule
    {
        private readonly Answer _answer;

        public RevokedAnswersCannotBeCheckedRule(Answer answer)
        {
            _answer = answer;
        }

        public string Message => $"Нельзя оценивать отозванный ответ (ID={_answer.ID})";

        public bool IsBroken => _answer.IsRevoked;
    }
}
