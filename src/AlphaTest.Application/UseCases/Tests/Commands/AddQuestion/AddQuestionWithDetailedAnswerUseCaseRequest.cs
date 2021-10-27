using System;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public class AddQuestionWithDetailedAnswerUseCaseRequest : AddQuestionUseCaseRequest
    {
        public AddQuestionWithDetailedAnswerUseCaseRequest(Guid testID, string text, int score) : base(testID, text, score) { }
    }
}
