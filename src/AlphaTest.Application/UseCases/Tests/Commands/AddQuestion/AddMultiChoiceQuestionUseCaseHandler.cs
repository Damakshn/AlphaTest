using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public class AddMultiChoiceQuestionUseCaseHandler : AddQuestionUseCaseHandler<AddMultichoiceQuestionUseCaseRequest, MultiChoiceQuestion>
    {
        public AddMultiChoiceQuestionUseCaseHandler(AlphaTestContext db) : base(db) { }

        protected override MultiChoiceQuestion AddQuestion(Test test, AddMultichoiceQuestionUseCaseRequest request, uint numberOfQuestionInTest)
        {
            MultiChoiceQuestion question = test.AddMultiChoiceQuestion(
                request.Text, request.Score, request.OptionsData, numberOfQuestionInTest);
            return question;
        }
    }
}
