using System;


namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public class EditQuestionWithDetailedAnswerUseCaseRequest : EditQuestionUseCaseRequest
    {
        public EditQuestionWithDetailedAnswerUseCaseRequest(Guid testID, Guid questionID, int? score, string text)
            : base(testID, questionID, score, text)
        {
        }
    }
}
