using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeTimeLimit
{
    public class ChangeTimeLimitUseCaseHandler : UseCaseHandlerBase<ChangeTimeLimitUseCaseRequest>
    {
        public ChangeTimeLimitUseCaseHandler(AlphaTestContext db) : base(db) { }

        public async override Task<Unit> Handle(ChangeTimeLimitUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeTimeLimit(request.TimeLimit);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
