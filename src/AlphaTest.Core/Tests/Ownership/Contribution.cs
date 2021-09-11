using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Users;
using AlphaTest.Core.Tests.Ownership.Rules;
namespace AlphaTest.Core.Tests.Ownership
{
    public class Contribution: Entity
    {
        private Contribution() { }

        internal Contribution(Test test, User teacher)
        {
            CheckRule(new OnlyTeacherCanBeSetAsNewAuthorOrContributorRule(teacher));
            CheckRule(new SuspendedUserCannotBeSetAsNewAuthorOrContributorRule(teacher));
            TestID = test.ID;
            TeacherID = teacher.ID;
        }

        public Contribution ReplicateForNewEdition(Test newEdition)
        {
            Contribution replica = (Contribution)this.MemberwiseClone();
            replica.TestID = newEdition.ID;
            return replica;
        }

        public int TestID { get; private set; }

        public int TeacherID { get; private set; }
    }
}
