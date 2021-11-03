using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public class EditQuestionWithNumericAnswerUseCaseHandler 
        : EditQuestionUseCaseHandler<EditQuestionWithNumericAnswerUseCaseRequest, QuestionWithNumericAnswer>
    {
        public EditQuestionWithNumericAnswerUseCaseHandler(AlphaTestContext db) : base(db)
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
