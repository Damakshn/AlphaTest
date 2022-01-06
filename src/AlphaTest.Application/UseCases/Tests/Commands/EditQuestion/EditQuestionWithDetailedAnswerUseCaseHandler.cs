using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.DataAccess.EF.Abstractions;


namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public class EditQuestionWithDetailedAnswerUseCaseHandler
        : EditQuestionUseCaseHandler<EditQuestionWithDetailedAnswerUseCaseRequest, QuestionWithDetailedAnswer>
    {
        public EditQuestionWithDetailedAnswerUseCaseHandler(IDbContext db) : base(db)
        {
        }

        protected override void EditQuestion(EditQuestionWithDetailedAnswerUseCaseRequest request, QuestionWithDetailedAnswer question, Test test)
        {
            base.EditQuestion(request, question, test);
        }
    }
}
