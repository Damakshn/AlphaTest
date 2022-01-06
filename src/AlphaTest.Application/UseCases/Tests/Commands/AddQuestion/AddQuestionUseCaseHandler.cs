using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.AddQuestion
{
    public abstract class AddQuestionUseCaseHandler<TAddQuestionRequest, TQuestion> 
        : UseCaseHandlerBase<TAddQuestionRequest, Guid> 
        where TAddQuestionRequest : AddQuestionUseCaseRequest 
        where TQuestion : Question
    {
        public AddQuestionUseCaseHandler(IDbContext db) : base(db) { }

        public override async Task<Guid> Handle(TAddQuestionRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            uint numberOfQuestionInTest = (uint)_db.Questions.FilterByTest(test.ID).Count();
            Question question = AddQuestion(test, request, numberOfQuestionInTest);
            _db.Questions.Add(question);
            await _db.SaveChangesAsync(cancellationToken);
            return question.ID;
        }

        protected abstract TQuestion AddQuestion(Test test, TAddQuestionRequest request, uint numberOfQuestionInTest);
    }
}
