using System;
using System.Collections.Generic;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public class AddSingleChoiceQuestionUseCaseRequest : AddQuestionWithChoicesUseCaseRequest
    {
        public AddSingleChoiceQuestionUseCaseRequest(
            Guid testID,
            string text,
            int score,
            List<(string text, uint number, bool isRight)> optionsData) : base(testID, text, score, optionsData)  { }
    }
}
