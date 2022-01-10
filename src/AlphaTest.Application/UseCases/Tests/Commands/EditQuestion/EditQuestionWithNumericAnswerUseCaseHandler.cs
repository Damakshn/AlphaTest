using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public class EditQuestionWithNumericAnswerUseCaseHandler 
        : EditQuestionUseCaseHandler<EditQuestionWithNumericAnswerUseCaseRequest, QuestionWithNumericAnswer>
    {
        public EditQuestionWithNumericAnswerUseCaseHandler(IDbContext db) : base(db)
        {
        }

        protected override void EditQuestion(EditQuestionWithNumericAnswerUseCaseRequest request, QuestionWithNumericAnswer question, Test test)
        {
            base.EditQuestion(request, question, test);
            if (request.NumericAnswer is not null)
                question.ChangeRightAnswer((decimal)request.NumericAnswer);
        }
    }
}
