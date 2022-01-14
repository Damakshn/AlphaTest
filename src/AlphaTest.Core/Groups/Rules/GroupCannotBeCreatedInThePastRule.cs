using System;
using AlphaTest.Core.Common;
using AlphaTest.Core.Common.Utils;

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

        public bool IsBroken => _beginDate < TimeResolver.CurrentTime;
    }
}
