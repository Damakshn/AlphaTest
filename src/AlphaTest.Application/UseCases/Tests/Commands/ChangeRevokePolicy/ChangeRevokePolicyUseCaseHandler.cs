using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.ChangeRevokePolicy
{
    public class ChangeRevokePolicyUseCaseHandler : UseCaseHandlerBase<ChangeRevokePolicyUseCaseRequest>
    {
        public ChangeRevokePolicyUseCaseHandler(IDbContext db): base(db) { }

        public async override Task<Unit> Handle(ChangeRevokePolicyUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.ChangeRevokePolicy(request.RevokePolicy);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
