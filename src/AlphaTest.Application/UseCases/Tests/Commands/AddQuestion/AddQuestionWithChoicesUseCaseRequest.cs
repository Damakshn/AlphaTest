using System;
using System.Collections.Generic;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public abstract class AddQuestionWithChoicesUseCaseRequest : AddQuestionUseCaseRequest
    {
        public AddQuestionWithChoicesUseCaseRequest(
            Guid testID,
            string text,
            int score,
            List<(string text, uint number, bool isRight)> optionsData) : base(testID, text, score)
        {
            OptionsData = optionsData;
        }

        public List<(string text, uint number, bool isRight)> OptionsData { get; protected set; }
        
    }
}
