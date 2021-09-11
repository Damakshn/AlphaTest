using AlphaTest.Core.Common;

namespace AlphaTest.Core.Examinations.Rules
{
    public class CanceledExaminationCannotBeModifiedRule : IBusinessRule
    {
        private readonly Examination _exam;

        public CanceledExaminationCannotBeModifiedRule(Examination examination)
        {
            _exam = examination;
        }
        public string Message => "Операция запрещена, так как экзамен отменён.";

        public bool IsBroken => _exam.IsCanceled;
    }
}
