using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeScoreDistribution
{
    public class ChangeScoreDistributionUseCaseHandler : UseCaseHandlerBase<ChangeScoreDistributionUseCaseRequest>
    {
        public ChangeScoreDistributionUseCaseHandler(IDbContext db) : base(db) { }

        public override async Task<Unit> Handle(ChangeScoreDistributionUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ConfigureScoreDistribution(request.ScoreDistributionMethod, request.Score);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
