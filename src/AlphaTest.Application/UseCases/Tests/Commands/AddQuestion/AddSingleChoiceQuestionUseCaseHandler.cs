using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public class AddSingleChoiceQuestionUseCaseHandler : AddQuestionUseCaseHandler<AddSingleChoiceQuestionUseCaseRequest, SingleChoiceQuestion>
    {
        public AddSingleChoiceQuestionUseCaseHandler(AlphaTestContext db) : base(db) { }

        protected override SingleChoiceQuestion AddQuestion(Test test, AddSingleChoiceQuestionUseCaseRequest request, uint numberOfQuestionInTest)
        {
            SingleChoiceQuestion question = test.AddSingleChoiceQuestion(
                request.Text,
                request.Score,
                request.OptionsData,
                numberOfQuestionInTest);
            return question;
        }
    }
}
