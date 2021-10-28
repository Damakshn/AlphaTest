using AlphaTest.Core.Common.Abstractions;
using System;

namespace AlphaTest.Core.Tests.Ownership
{
    public class Contribution: Entity
    {
        private Contribution() { }

        internal Contribution(Test test, Guid teacherID)
        {   
            TestID = test.ID;
            TeacherID = teacherID;
        }

        public Contribution ReplicateForNewEdition(Test newEdition)
        {
            Contribution replica = (Contribution)this.MemberwiseClone();
            replica.TestID = newEdition.ID;
            return replica;
        }

        public Guid TestID { get; private set; }

        public Guid TeacherID { get; private set; }
    }
}
