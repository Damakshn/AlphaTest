using System;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public abstract class AddQuestionWithExactAnswerUseCaseRequest<TValue> : AddQuestionUseCaseRequest
    {
        public AddQuestionWithExactAnswerUseCaseRequest(Guid testID, string text, int score, TValue rightAnswer)
            : base(testID, text, score)
        {
            RightAnswer = rightAnswer;
        }

        public TValue RightAnswer { get; protected set; }
    }
}
