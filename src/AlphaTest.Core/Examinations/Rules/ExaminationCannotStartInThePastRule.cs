using System;
using AlphaTest.Core.Common;

namespace AlphaTest.Core.Examinations.Rules
{
    public class ExaminationCannotStartInThePastRule : IBusinessRule
    {
        private readonly DateTime _start;

        public ExaminationCannotStartInThePastRule(DateTime start)
        {
            _start = start;
        }
        public string Message => "Экзамен не может начинаться в прошлом.";

        public bool IsBroken => _start < DateTime.Now;
    }
}
