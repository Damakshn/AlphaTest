using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangePassingScore
{
    public class ChangePassingScoreUseCaseHandler : UseCaseHandlerBase<ChangePassingScoreUseCaseRequest>
    {
        public ChangePassingScoreUseCaseHandler(AlphaTestContext db) : base(db) { }

        public override async Task<Unit> Handle(ChangePassingScoreUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangePassingScore(request.NewScore);
            return Unit.Value;
        }
    }
}
