using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.EditQuestion
{
    public abstract class EditQuestionUseCaseHandler<TEditQuestionRequest, TQuestion> 
        :UseCaseHandlerBase<TEditQuestionRequest> 
        where TEditQuestionRequest : EditQuestionUseCaseRequest 
        where TQuestion : Question
    {
        public EditQuestionUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(TEditQuestionRequest request, CancellationToken cancellationToken)
        {
            Question question = await _db.Questions.Aggregates().FindByID(request.QuestionID);
            ThrowIfIncorrectQuestionType(question);
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            EditQuestion(request, question as TQuestion, test);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        private void ThrowIfIncorrectQuestionType(Question question)
        {
            if (question is not TQuestion)
                throw new AlphaTestApplicationException("Тип вопроса не соответствует выполняемой операции.");
        }

        protected virtual void EditQuestion(TEditQuestionRequest request, TQuestion question, Test test)
        {
            if (request.Score is not null)
                question.ChangeScore(test, request.Score);
            if (request.Text is not null)
                question.ChangeText(test, request.Text);
        }
    }
}
