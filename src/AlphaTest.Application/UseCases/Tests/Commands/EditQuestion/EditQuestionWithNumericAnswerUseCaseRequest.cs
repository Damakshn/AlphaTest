using System;

namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public class EditQuestionWithNumericAnswerUseCaseRequest : EditQuestionUseCaseRequest
    {
        public EditQuestionWithNumericAnswerUseCaseRequest(Guid testID, Guid questionID, int? score, string text, decimal? numericAnswer) : 
            base(testID, questionID, score, text)
        {
            NumericAnswer = numericAnswer;
        }

        public decimal? NumericAnswer { get; private set; }
    }
}
