using System;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public class AddQuestionWithNumericAnswerUseCaseRequest : AddQuestionWithExactAnswerUseCaseRequest<decimal>
    {
        public AddQuestionWithNumericAnswerUseCaseRequest(Guid testID, string text, int score, decimal rightAnswer)
            : base(testID, text, score, rightAnswer) { }
    }
}
