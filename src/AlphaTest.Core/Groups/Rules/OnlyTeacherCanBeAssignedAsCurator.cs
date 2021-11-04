using AlphaTest.Core.Common;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Groups.Rules
{
    public class OnlyTeacherCanBeAssignedAsCurator : IBusinessRule
    {
        private readonly IAlphaTestUser _user;
        public OnlyTeacherCanBeAssignedAsCurator(IAlphaTestUser user)
        {
            _user = user;
        }

        public string Message => "Куратором группы может быть назначен только преподаватель.";

        public bool IsBroken => _user.IsTeacher == false;
    }
}
