using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public class AddQuestionWithDetailedAnswerUseCaseHandler : AddQuestionUseCaseHandler<AddQuestionWithDetailedAnswerUseCaseRequest, QuestionWithDetailedAnswer>
    {
        public AddQuestionWithDetailedAnswerUseCaseHandler(IDbContext db) : base(db) { }

        protected override QuestionWithDetailedAnswer AddQuestion(Test test, AddQuestionWithDetailedAnswerUseCaseRequest request, uint numberOfQuestionInTest)
        {
            QuestionWithDetailedAnswer question = test.AddQuestionWithDetailedAnswer(request.Text, request.Score, numberOfQuestionInTest);
            return question;
        }
    }
}
