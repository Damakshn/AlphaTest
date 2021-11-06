using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Examinations.Commands.StartWork
{
    public class StartWorkUseCaseRequest : IUseCaseRequest
    {
        public StartWorkUseCaseRequest(Guid studentID, Guid examinationID)
        {
            StudentID = studentID;
            ExaminationID = examinationID;
        }

        public Guid StudentID { get; private set; }

        public Guid ExaminationID { get; private set; }
    }
}
