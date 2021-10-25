using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.Core.Tests;
using AlphaTest.Application.Exceptions;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeRevokePolicy
{
    public class ChangeRevokePolicyUseCaseHandler : IRequestHandler<ChangeRevokePolicyUseCaseRequest>
    {
        private AlphaTestContext _db;

        public ChangeRevokePolicyUseCaseHandler(AlphaTestContext db)
        {
            _db = db;
        }

        public async Task<Unit> Handle(ChangeRevokePolicyUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeRevokePolicy(request.RevokePolicy);
            return Unit.Value;
        }
    }
}
