using AlphaTest.Core.Common;

namespace AlphaTest.Core.Groups.Rules
{
    public class GroupMustBeUniqueRule : IBusinessRule
    {
        private readonly bool _groupAlreadyExists;

        public GroupMustBeUniqueRule(bool groupAlreadyExists)
        {
            _groupAlreadyExists = groupAlreadyExists;
        }

        public string Message => "Такая группа уже есть.";

        public bool IsBroken => _groupAlreadyExists;
    }
}
