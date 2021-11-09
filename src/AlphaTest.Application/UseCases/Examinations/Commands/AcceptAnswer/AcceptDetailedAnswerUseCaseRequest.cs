using System;

namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public class AcceptDetailedAnswerUseCaseRequest : AcceptAnswerUseCaseRequest
    {
        public AcceptDetailedAnswerUseCaseRequest(Guid examinationID, Guid studentID, Guid questionID, string detailedAnswer) 
            : base(examinationID, studentID, questionID)
        {
            DetailedAnswer = detailedAnswer;
        }

        public string DetailedAnswer { get; private set; }
    }
}
