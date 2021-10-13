using AlphaTest.Core.Common;
using AlphaTest.Core.Users;


namespace AlphaTest.Core.Tests.Ownership.Rules
{
    public class OnlyTeacherCanBeSetAsNewAuthorOrContributorRule : IBusinessRule
    {
        private readonly IAlphaTestUser _user;

        public OnlyTeacherCanBeSetAsNewAuthorOrContributorRule(IAlphaTestUser user)
        {
            _user = user;
        }

        public string Message => "Только преподаватель может быть назначен автором теста.";

        public bool IsBroken => _user.IsTeacher == false;
    }
}
