using AlphaTest.Core.Common;
using AlphaTest.Core.Common.Utils;

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

        public bool IsBroken => _exam.StartsAt < TimeResolver.CurrentTime;
    }
}
