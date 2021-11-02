using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Examinations.Commands.ChangeExaminationTerms
{
    public class ChangeExaminationTermsUseCaseRequest : IUseCaseRequest
    {
        public ChangeExaminationTermsUseCaseRequest(Guid examinationID, DateTime startsAt, DateTime endsAt)
        {
            ExaminationID = examinationID;
            StartsAt = startsAt;
            EndsAt = endsAt;
        }

        public Guid ExaminationID { get; private set; }

        public DateTime StartsAt { get; private set; }

        public DateTime EndsAt { get; private set; }
    }
}
