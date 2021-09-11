using System;
using AlphaTest.Core.Common;

namespace AlphaTest.Core.Examinations.Rules
{
    public class ExaminationCannotBeCreatedThePastRule : IBusinessRule
    {
        private readonly DateTime _start;
        private readonly DateTime _end;

        public ExaminationCannotBeCreatedThePastRule(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
        }
        public string Message => "Нельзя назначить экзамен в прошлом.";

        public bool IsBroken => _start < DateTime.Now || _end < DateTime.Now;
    }
}
