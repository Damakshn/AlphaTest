using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public class AddQuestionWithNumericAnswerUseCaseHandler : AddQuestionUseCaseHandler<AddQuestionWithNumericAnswerUseCaseRequest, QuestionWithNumericAnswer>
    {
        public AddQuestionWithNumericAnswerUseCaseHandler(IDbContext db) : base(db) { }

        protected override QuestionWithNumericAnswer AddQuestion(Test test, AddQuestionWithNumericAnswerUseCaseRequest request, uint numberOfQuestionInTest)
        {
            QuestionWithNumericAnswer question = test.AddQuestionWithNumericAnswer(
                request.Text, request.Score, request.RightAnswer, numberOfQuestionInTest);
            return question;
        }
    }
}
