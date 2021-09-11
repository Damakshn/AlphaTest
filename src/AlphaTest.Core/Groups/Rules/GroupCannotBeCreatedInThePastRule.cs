using System;
using AlphaTest.Core.Common;

namespace AlphaTest.Core.Groups.Rules
{
    public class GroupCannotBeCreatedInThePastRule : IBusinessRule
    {
        private readonly DateTime _beginDate;

        public GroupCannotBeCreatedInThePastRule(DateTime beginDate)
        {
            _beginDate = beginDate;
        }

        public string Message => "Группа не может быть создана задним числом.";

        public bool IsBroken => _beginDate < DateTime.Now;
    }
}
