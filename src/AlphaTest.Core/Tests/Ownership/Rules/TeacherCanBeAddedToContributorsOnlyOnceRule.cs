using AlphaTest.Core.Common;
using AlphaTest.Core.Users;
using System.Linq;

namespace AlphaTest.Core.Tests.Ownership.Rules
{
    public class TeacherCanBeAddedToContributorsOnlyOnceRule : IBusinessRule
    {
        private readonly IAlphaTestUser _candidate;

        private readonly Test _test;

        public TeacherCanBeAddedToContributorsOnlyOnceRule(IAlphaTestUser candidate, Test test)
        {
            _candidate = candidate;
            _test = test;
        }

        // ToDo сделать красиво
        public string Message => "Преподаватель уже включён в список составителей";

        public bool IsBroken => _test.Contributions.Count(c => c.TeacherID == _candidate.ID) > 0;
    }
}
