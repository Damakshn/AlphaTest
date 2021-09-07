using AlphaTest.Core.Common;
using AlphaTest.Core.Users;


namespace AlphaTest.Core.Tests.Ownership.Rules
{
    public class OnlyTeacherCanBeSetAsNewAuthorOrContributorRule : IBusinessRule
    {
        private readonly User _user;

        public OnlyTeacherCanBeSetAsNewAuthorOrContributorRule(User user)
        {
            _user = user;
        }

        public string Message => "Только преподаватель может быть назначен автором теста.";

        public bool IsBroken => _user.IsInRole(UserRole.TEACHER) == false;
    }
}
