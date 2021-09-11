using AlphaTest.Core.Common;

namespace AlphaTest.Core.Groups.Rules
{
    public class GroupNameMustBeProvidedRule : IBusinessRule
    {
        private readonly string _groupName;

        public GroupNameMustBeProvidedRule(string groupName)
        {
            _groupName = groupName;
        }

        public string Message => "Имя группы должно быть указано.";

        public bool IsBroken => string.IsNullOrWhiteSpace(_groupName);
    }
}
