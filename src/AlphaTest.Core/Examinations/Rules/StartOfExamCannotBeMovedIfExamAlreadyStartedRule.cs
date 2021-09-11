using AlphaTest.Core.Common;
using System;

namespace AlphaTest.Core.Examinations.Rules
{
    public class StartOfExamCannotBeMovedIfExamAlreadyStartedRule : IBusinessRule
    {
        private readonly Examination _exam;

        public StartOfExamCannotBeMovedIfExamAlreadyStartedRule(Examination exam)
        {
            _exam = exam;
        }

        public string Message => "Нельзя изменить дату начала экзамена, так как экзамен уже начался.";

        public bool IsBroken => _exam.StartsAt < DateTime.Now;
    }
}
