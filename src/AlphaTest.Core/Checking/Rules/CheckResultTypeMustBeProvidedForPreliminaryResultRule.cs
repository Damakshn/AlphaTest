using AlphaTest.Core.Common;


namespace AlphaTest.Core.Checking.Rules
{
    public class CheckResultTypeMustBeProvidedForPreliminaryResultRule : IBusinessRule
    {
        private readonly CheckResultType _checkResultType;

        public CheckResultTypeMustBeProvidedForPreliminaryResultRule(CheckResultType checkResultType)
        {
            _checkResultType = checkResultType;
        }

        public string Message => "Должна быть указана оценка ответа.";

        public bool IsBroken => _checkResultType is null;
    }
}
