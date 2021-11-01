using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Examinations.Commands.CancelExamination
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
