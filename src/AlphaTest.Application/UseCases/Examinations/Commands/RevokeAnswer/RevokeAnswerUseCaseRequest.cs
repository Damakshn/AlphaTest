using System;
using AlphaTest.Application.UseCases.Common;


namespace AlphaTest.Application.UseCases.Examinations.Commands.RevokeAnswer
{
    public class RevokeAnswerUseCaseRequest : IUseCaseRequest
    {
        public RevokeAnswerUseCaseRequest(Guid examinationID, Guid questionID, Guid studentID)
        {
            ExaminationID = examinationID;
            QuestionID = questionID;
            StudentID = studentID;
        }

        public Guid ExaminationID { get; private set; }

        public Guid QuestionID { get; private set; }

        public Guid StudentID { get; private set; }
    }
}
