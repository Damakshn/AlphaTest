using System;
using AlphaTest.Application.UseCases.Common;


namespace AlphaTest.Application.UseCases.Examinations.Commands.FinishWork
{
    public class FinishCurrentWorkUseCaseRequest : IUseCaseRequest
    {
        public FinishCurrentWorkUseCaseRequest(Guid examinationID, Guid studentID)
        {
            ExaminationID = examinationID;
            StudentID = studentID;
        }

        public Guid ExaminationID { get; private set; }

        public Guid StudentID { get; private set; }
    }
}
