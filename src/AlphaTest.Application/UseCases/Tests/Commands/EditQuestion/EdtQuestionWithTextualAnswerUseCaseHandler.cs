using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public class EdtQuestionWithTextualAnswerUseCaseHandler
        : EditQuestionUseCaseHandler<EditQuestionWithTextualAnswerUseCaseRequest, QuestionWithTextualAnswer>
    {
        public EdtQuestionWithTextualAnswerUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        protected override void EditQuestion(EditQuestionWithTextualAnswerUseCaseRequest request, QuestionWithTextualAnswer question, Test test)
        {
            base.EditQuestion(request, question, test);
            if (request.TextualAnswer is not null)
                question.ChangeRightAnswer(request.TextualAnswer);
        }
    }
}
