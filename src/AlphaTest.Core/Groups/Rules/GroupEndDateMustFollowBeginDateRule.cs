using AlphaTest.Core.Common;
using System;

namespace AlphaTest.Core.Groups.Rules
{
    public class GroupEndDateMustFollowBeginDateRule : IBusinessRule
    {

        private readonly DateTime _beginDate;

        private readonly DateTime _endDate;

        public GroupEndDateMustFollowBeginDateRule(DateTime beginDate, DateTime endDate)
        {
            _beginDate = beginDate;
            _endDate = endDate;
        }

        public string Message => "Дата начала обучения не должна идти после даты окончания.";

        public bool IsBroken => _beginDate > _endDate;
    }
}
