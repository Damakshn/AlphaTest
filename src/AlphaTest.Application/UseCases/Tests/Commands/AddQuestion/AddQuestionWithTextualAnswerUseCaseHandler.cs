using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public class AddQuestionWithTextualAnswerUseCaseHandler : AddQuestionUseCaseHandler<AddQuestionWithTextualAnswerUseCaseRequest, QuestionWithTextualAnswer>
    {
        public AddQuestionWithTextualAnswerUseCaseHandler(AlphaTestContext db) : base(db) { }

        protected override QuestionWithTextualAnswer AddQuestion(Test test, AddQuestionWithTextualAnswerUseCaseRequest request, uint numberOfQuestionInTest)
        {
            QuestionWithTextualAnswer question = test.AddQuestionWithTextualAnswer(
                request.Text, request.Score, request.RightAnswer, numberOfQuestionInTest);
            return question;
        }
    }
}
