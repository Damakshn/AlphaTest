using AlphaTest.Core.Common;
using AlphaTest.Core.Examinations;

namespace AlphaTest.Core.Works.Rules
{
    public class NewWorkCannotBeStartedIfExaminationIsAreadyEndedRule : IBusinessRule
    {
        private readonly Examination _examination;

        public NewWorkCannotBeStartedIfExaminationIsAreadyEndedRule(Examination examination)
        {
            _examination = examination;
        }

        public string Message => $"Невозможно начать тестирование, так как срок приёма работ закончился {_examination.EndsAt.Date} в {_examination.EndsAt.TimeOfDay}.";

        public bool IsBroken => _examination.IsEnded;
    }
}
