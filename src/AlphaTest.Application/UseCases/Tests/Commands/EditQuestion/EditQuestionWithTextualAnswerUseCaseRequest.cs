using System;

namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public class EditQuestionWithTextualAnswerUseCaseRequest : EditQuestionUseCaseRequest
    {
        public EditQuestionWithTextualAnswerUseCaseRequest(Guid testID, Guid questionID, int? score, string text, string textualAnswer) 
            :base(testID, questionID, score, text)
        {
            TextualAnswer = textualAnswer;
        }

        public string TextualAnswer { get; private set; }
    }
}
