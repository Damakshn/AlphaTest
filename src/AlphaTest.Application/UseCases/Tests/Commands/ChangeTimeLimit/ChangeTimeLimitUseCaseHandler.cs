using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.Core.Tests;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeTimeLimit
{
    public class ChangeTimeLimitUseCaseHandler : IRequestHandler<ChangeTimeLimitUseCaseRequest>
    {
        private AlphaTestContext _db;

        public ChangeTimeLimitUseCaseHandler(AlphaTestContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(ChangeTimeLimitUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeTimeLimit(request.TimeLimit);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
