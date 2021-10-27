using System;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public class AddQuestionWithTextualAnswerUseCaseRequest : AddQuestionWithExactAnswerUseCaseRequest<string>
    {
        public AddQuestionWithTextualAnswerUseCaseRequest(Guid testID, string text, int score, string rightAnswer)
            : base(testID, text, score, rightAnswer) { }
    }
}
