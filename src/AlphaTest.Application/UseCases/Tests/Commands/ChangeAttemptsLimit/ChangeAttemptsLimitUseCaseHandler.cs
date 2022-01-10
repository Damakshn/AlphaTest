using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeAttemptsLimit
{
    public class ChangeAttemptsLimitUseCaseHandler : UseCaseHandlerBase<ChangeAttemptsLimitUseCaseRequest>
    {
        public ChangeAttemptsLimitUseCaseHandler(IDbContext db) : base(db) { }

        public override async Task<Unit> Handle(ChangeAttemptsLimitUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeAttemptsLimit(request.AttemptsLimit);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
