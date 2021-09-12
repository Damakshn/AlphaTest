using System.Linq;
using AlphaTest.Core.Common;


namespace AlphaTest.Core.Tests.Ownership.Rules
{
    public class NonContributorTeacherCannotBeRemovedFromContributorsRule : IBusinessRule
    {
        private readonly int _contributorID;

        private readonly Test _test;

        public NonContributorTeacherCannotBeRemovedFromContributorsRule(int contributorID, Test test)
        {
            _contributorID = contributorID;
            _test = test;
        }

        // ToDo сделать красиво
        public string Message => "Данный преподаватель не входит в число составителей, поэтому его нельзя исключить из списка.";

        public bool IsBroken => _test.Contributions.Count(c => c.TeacherID == _contributorID) == 0;
    }
}
