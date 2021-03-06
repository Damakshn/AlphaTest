using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeWorkCheckingMethod
{
    public class ChangeWorkCheckingMethodUseCaseHandler : UseCaseHandlerBase<ChangeWorkCheckingMethodUseCaseRequest>
    {
        public ChangeWorkCheckingMethodUseCaseHandler(IDbContext db) : base(db) { }

        public override async Task<Unit> Handle(ChangeWorkCheckingMethodUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            List<Question> allQuestionsInTest = await _db.Questions.Aggregates().FilterByTest(test.ID).ToListAsync();
            test.ChangeWorkCheckingMethod(request.NewWorkCheckingMethod, allQuestionsInTest);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
