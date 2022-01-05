using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeAttemptsLimit
{
    public class ChangeAttemptsLimitUseCaseHandler : UseCaseHandlerBase<ChangeAttemptsLimitUseCaseRequest>
    {
        public ChangeAttemptsLimitUseCaseHandler(AlphaTestContext db) : base(db) { }

        public override async Task<Unit> Handle(ChangeAttemptsLimitUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeAttemptsLimit(request.AttemptsLimit);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
