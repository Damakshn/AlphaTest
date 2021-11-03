using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;


namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public class EditQuestionWithDetailedAnswerUseCaseHandler
        : EditQuestionUseCaseHandler<EditQuestionWithDetailedAnswerUseCaseRequest, QuestionWithDetailedAnswer>
    {
        public EditQuestionWithDetailedAnswerUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        protected override void EditQuestion(EditQuestionWithDetailedAnswerUseCaseRequest request, QuestionWithDetailedAnswer question, Test test)
        {
            base.EditQuestion(request, question, test);
        }
    }
}
