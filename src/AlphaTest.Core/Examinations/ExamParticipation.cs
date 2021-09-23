using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Groups;
using System;

namespace AlphaTest.Core.Examinations
{
    public class ExamParticipation: Entity
    {
        private ExamParticipation() { }

        internal ExamParticipation(Examination examination, Group group)
        {
            ExaminationID = examination.ID;
            GroupID = group.ID;
        }

        public Guid ExaminationID { get; private set; }

        public int GroupID { get; private set; }
    }
}
