using System;

namespace AlphaTest.Application.UseCases.Examinations.Commands.AcceptAnswer
{
    public class AcceptTextualAnswerUseCaseRequest : AcceptAnswerUseCaseRequest
    {
        public AcceptTextualAnswerUseCaseRequest(Guid examinationID, Guid studentID, Guid questionID, string textualAnswer) 
            : base(examinationID, studentID, questionID)
        {
            TextualAnswer = textualAnswer;
        }

        public string TextualAnswer { get; private set; }
    }
}
