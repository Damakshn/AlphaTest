using System;
using System.Collections.Generic;

namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public class EditQuestionWithChoicesUseCaseRequest : EditQuestionUseCaseRequest
    {
        public EditQuestionWithChoicesUseCaseRequest(
            Guid testID, 
            Guid questionID, 
            int? score, 
            string text, 
            List<(string text, uint number, bool isRight)> optionsData) 
            :base(testID, questionID, score, text)
        {
            OptionsData = optionsData;
        }

        public List<(string text, uint number, bool isRight)> OptionsData { get; private set; }
    }
}
