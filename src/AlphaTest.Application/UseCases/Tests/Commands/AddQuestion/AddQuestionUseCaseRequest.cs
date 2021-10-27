using System;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public abstract class AddQuestionUseCaseRequest : IUseCaseRequest<Guid>
    {
        public AddQuestionUseCaseRequest(Guid testID, string text, int score)
        {
            TestID = testID;
            Text = new(text);
            Score = new(score);
        }

        public Guid TestID { get; protected set; }

        public QuestionText Text { get; protected set; }

        public QuestionScore Score { get; protected set; }
    }
}
