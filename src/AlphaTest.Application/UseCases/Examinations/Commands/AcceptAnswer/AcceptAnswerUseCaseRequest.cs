using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public abstract class AcceptAnswerUseCaseRequest : IUseCaseRequest
    {
        public AcceptAnswerUseCaseRequest(Guid examinationID, Guid studentID, Guid questionID)
        {
            ExaminationID = examinationID;
            StudentID = studentID;
            QuestionID = questionID;
        }
        
        public Guid ExaminationID { get; private set; }

        public Guid StudentID { get; private set; }

        public Guid QuestionID { get; private set; }

    }
}
