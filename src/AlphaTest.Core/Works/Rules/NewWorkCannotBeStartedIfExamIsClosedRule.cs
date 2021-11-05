using AlphaTest.Core.Common;
using AlphaTest.Core.Examinations;

namespace AlphaTest.Core.Works.Rules
{
    public class NewWorkCannotBeStartedIfExamIsClosedRule : IBusinessRule
    {
        private readonly Examination _examination;

        public NewWorkCannotBeStartedIfExamIsClosedRule(Examination examination)
        {
            _examination = examination;
        }

        public string Message => "Нельзя начать тестирование, так как экзамен был отменён.";

        public bool IsBroken => _examination.IsCanceled;
    }
}
