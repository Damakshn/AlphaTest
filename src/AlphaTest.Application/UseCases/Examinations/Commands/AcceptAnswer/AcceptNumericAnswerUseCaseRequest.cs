using System;

namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public class AcceptNumericAnswerUseCaseRequest : AcceptAnswerUseCaseRequest
    {
        public AcceptNumericAnswerUseCaseRequest(Guid examinationID, Guid studentID, Guid questionID, decimal numericAnswer) 
            : base(examinationID, studentID, questionID)
        {
            NumericAnswer = numericAnswer;
        }

        public decimal NumericAnswer { get; private set; }
    }
}
