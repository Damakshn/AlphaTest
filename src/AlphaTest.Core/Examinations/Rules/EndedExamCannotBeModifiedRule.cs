using System;
using AlphaTest.Core.Common;

namespace AlphaTest.Core.Examinations.Rules
{
    public class EndedExamCannotBeModifiedRule : IBusinessRule
    {
        private readonly Examination _exam;

        public EndedExamCannotBeModifiedRule(Examination exam)
        {
            _exam = exam;
        }

        public string Message => "Операция запрещена, так как экзамен уже прошёл.";

        public bool IsBroken => _exam.EndsAt < DateTime.Now;
    }
}
