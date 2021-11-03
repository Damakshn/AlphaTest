using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public class EditQuestionWithChoicesUseCaseHandler : EditQuestionUseCaseHandler<EditQuestionWithChoicesUseCaseRequest, QuestionWithChoices>
    {
        public EditQuestionWithChoicesUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        protected override void EditQuestion(EditQuestionWithChoicesUseCaseRequest request, QuestionWithChoices question, Test test)
        {
            base.EditQuestion(request, question, test);
            if (request.OptionsData is not null)
            {
                _db.QuestionOptions.RemoveRange(question.Options);
                question.ChangeOptions(request.OptionsData);
                _db.QuestionOptions.AddRange(question.Options);
            }
                
            
        }
    }
}
