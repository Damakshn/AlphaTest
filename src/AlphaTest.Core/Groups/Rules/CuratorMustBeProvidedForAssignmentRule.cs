using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Groups.Rules
{
    class CuratorMustBeProvidedForAssignmentRule : IBusinessRule
    {
        private readonly IAlphaTestUser _curator;

        public CuratorMustBeProvidedForAssignmentRule(IAlphaTestUser user)
        {
            _curator = user;
        }

        public string Message => "Куратор группы должен быть указан.";

        public bool IsBroken => _curator is null;
    }
}
