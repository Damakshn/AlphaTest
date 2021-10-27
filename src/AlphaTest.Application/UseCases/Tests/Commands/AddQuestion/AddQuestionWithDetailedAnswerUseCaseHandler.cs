using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public class AddQuestionWithDetailedAnswerUseCaseHandler : AddQuestionUseCaseHandler<AddQuestionWithDetailedAnswerUseCaseRequest, QuestionWithDetailedAnswer>
    {
        public AddQuestionWithDetailedAnswerUseCaseHandler(AlphaTestContext db) : base(db) { }

        protected override QuestionWithDetailedAnswer AddQuestion(Test test, AddQuestionWithDetailedAnswerUseCaseRequest request, uint numberOfQuestionInTest)
        {
            QuestionWithDetailedAnswer question = test.AddQuestionWithDetailedAnswer(request.Text, request.Score, numberOfQuestionInTest);
            return question;
        }
    }
}
