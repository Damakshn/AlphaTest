using System;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public class EditQuestionUseCaseRequest : IUseCaseRequest
    {
        public EditQuestionUseCaseRequest(
            Guid testID, 
            Guid questionID, 
            int? score,
            string text)
        {
            TestID = testID;
            QuestionID = questionID;
            Score = score is null ? null : new QuestionScore((int)score);
            Text = text is null ? null : new QuestionText(text);
        }

        public Guid TestID { get; private set; }

        public Guid QuestionID { get; private set; }

        public QuestionScore Score { get; private set; }

        public QuestionText Text { get; private set; }
    }
}
