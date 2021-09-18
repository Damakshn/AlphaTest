using System.Linq;
using AlphaTest.Core.Common;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers.Rules
{
    public class SingleChoiceAnswerValueMustBeValidOptionIDRule : IBusinessRule
    {
        private readonly SingleChoiceQuestion _question;

        private readonly int _value;

        public SingleChoiceAnswerValueMustBeValidOptionIDRule(SingleChoiceQuestion question, int value)
        {
            _question = question;
            _value = value;
        }

        // ToDo подумать над текстом сообщения
        public string Message => $"Невозможно зарегистрировать ответ на вопрос - вариант ответа с ID {_value} не найден.";

        public bool IsBroken => !_question.Options.Any(o => o.ID == _value);
    }
}
