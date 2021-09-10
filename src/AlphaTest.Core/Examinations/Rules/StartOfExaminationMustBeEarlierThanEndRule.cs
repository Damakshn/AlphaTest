using System;
using AlphaTest.Core.Common;

namespace AlphaTest.Core.Examinations.Rules
{
    public class StartOfExaminationMustBeEarlierThanEndRule : IBusinessRule
    {
        private readonly DateTime _start;

        private readonly DateTime _end;

        public StartOfExaminationMustBeEarlierThanEndRule(DateTime start, DateTime end)
        {
            _start = start;
            _end = end;
        }

        public string Message => "Начало экзамена не может стоять позже окончания.";

        public bool IsBroken => _start > _end;
    }
}
