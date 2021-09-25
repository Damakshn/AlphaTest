using System;
using System.Collections.Generic;
using System.Linq;
using AlphaTest.Core.Common;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers.Rules
{
    public class MultiChoiceAnswerValueMustBeValidSetOfOptionIDsRule : IBusinessRule
    {
        private readonly MultiChoiceQuestion _question;

        private readonly List<Guid> _value;

        public MultiChoiceAnswerValueMustBeValidSetOfOptionIDsRule(MultiChoiceQuestion question, List<Guid> value)
        {
            _question = question;
            _value = value;
        }

        // ToDo нужна подробная опись
        public string Message => "Невозможно зарегистрировать ответ - набор вариантов ответа некорректный.";

        public bool IsBroken => _value.Any(optionID => _question.Options.Where(o => o.ID == optionID).FirstOrDefault() is null);
    }
}
