using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public class AddQuestionWithTextualAnswerUseCaseHandler : AddQuestionUseCaseHandler<AddQuestionWithTextualAnswerUseCaseRequest, QuestionWithTextualAnswer>
    {
        public AddQuestionWithTextualAnswerUseCaseHandler(IDbContext db) : base(db) { }

        protected override QuestionWithTextualAnswer AddQuestion(Test test, AddQuestionWithTextualAnswerUseCaseRequest request, uint numberOfQuestionInTest)
        {
            QuestionWithTextualAnswer question = test.AddQuestionWithTextualAnswer(
                request.Text, request.Score, request.RightAnswer, numberOfQuestionInTest);
            return question;
        }
    }
}
