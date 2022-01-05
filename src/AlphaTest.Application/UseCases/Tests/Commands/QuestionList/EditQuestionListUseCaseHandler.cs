using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.QuestionList
{
    public abstract class EditQuestionListUseCaseHandler<TRequest> : UseCaseHandlerBase<TRequest> where TRequest : EditQuestionListUseCaseRequest
    {
        public EditQuestionListUseCaseHandler(AlphaTestContext db) : base(db) { }

        public override async Task<Unit> Handle(TRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            List<Question> questions = await _db.Questions.Aggregates().FilterByTest(test.ID).SortByNumber().ToListAsync();
            ExecuteAction(questions, request);
            ReorderQuestions(questions);
            await _db.SaveChangesAsync();
            return Unit.Value;
        }

        public void ReorderQuestions(List<Question> questions)
        {
            for (int i = 0; i < questions.Count; i++)
            {
                if (questions[i].Number != (i + 1))
                    questions[i].ChangeNumber((uint)i + 1);
            }
        }

        public abstract void ExecuteAction(List<Question> questions, TRequest request);
    }
}
