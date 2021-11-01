using System;
using System.Collections.Generic;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Examinations.Commands.CreateExamination
{
    public class CreateExaminationUseCaseRequest : IUseCaseRequest<Guid>
    {
        public CreateExaminationUseCaseRequest(Guid testID, DateTime startsAt, DateTime endsAt, Guid userID, List<Guid> groups)
        {
            TestID = testID;
            StartsAt = startsAt;
            EndsAt = endsAt;
            UserID = userID;
            Groups = groups;
        }

        public Guid TestID { get; private set; }

        public DateTime StartsAt { get; private set; }

        public DateTime EndsAt { get; private set; }

        public Guid UserID { get; private set; }

        public List<Guid> Groups { get; private set; }

    }
}
