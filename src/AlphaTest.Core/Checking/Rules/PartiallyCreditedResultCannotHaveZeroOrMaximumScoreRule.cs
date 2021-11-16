using AlphaTest.Core.Common;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Checking.Rules
{
    class PartiallyCreditedResultCannotHaveZeroOrMaximumScoreRule : IBusinessRule
    {
        private readonly CheckResultType _checkResultType;
        private readonly decimal _score;
        private readonly Question _question;

        public PartiallyCreditedResultCannotHaveZeroOrMaximumScoreRule(Question question, decimal score, CheckResultType checkResultType)
        {
            _checkResultType = checkResultType;
            _score = score;
            _question = question;
        }
       
        public string Message => $"Частично верный ответ не может быть оценён на максимальный балл или на 0 баллов.";

        public bool IsBroken => 
            _checkResultType == CheckResultType.PartiallyCredited &&
            (_score == 0 || _score == _question.Score.Value);
    }
}
