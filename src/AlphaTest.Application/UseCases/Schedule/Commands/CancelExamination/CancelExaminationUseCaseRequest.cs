using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Schedule.Commands.CancelExamination
{
    public class CancelExaminationUseCaseRequest : IUseCaseRequest
    {
        public CancelExaminationUseCaseRequest(Guid examinationID)
        {
            ExaminationID = examinationID;
        }

        public Guid ExaminationID { get; private set; }
    }
}
