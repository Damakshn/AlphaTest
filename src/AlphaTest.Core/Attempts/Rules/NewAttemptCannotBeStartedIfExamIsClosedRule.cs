using AlphaTest.Core.Common;
using AlphaTest.Core.Examinations;

namespace AlphaTest.Core.Attempts.Rules
{
    public class NewAttemptCannotBeStartedIfExamIsClosedRule : IBusinessRule
    {
        private readonly Examination _examination;

        public NewAttemptCannotBeStartedIfExamIsClosedRule(Examination examination)
        {
            _examination = examination;
        }

        public string Message => "Нельзя начать тестирование, так как экзамен был отменён.";

        public bool IsBroken => _examination.IsCanceled;
    }
}
