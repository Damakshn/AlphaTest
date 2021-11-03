using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Admin.Commands.Examinations.SwitchExaminer
{
    public class SwitchExaminerUseCaseRequest : IUseCaseRequest
    {
        public SwitchExaminerUseCaseRequest(Guid examinationID, Guid examinerID)
        {
            ExaminationID = examinationID;
            ExaminerID = examinerID;
        }

        public Guid ExaminationID { get; private set; }

        public Guid ExaminerID { get; private set; }
    }
}
