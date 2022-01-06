using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Tests;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Tests.Commands.RemoveContributor
{
    public class RemoveContributorUseCaseHandler : UseCaseHandlerBase<RemoveContributorUseCaseRequest>
    {
        public RemoveContributorUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(RemoveContributorUseCaseRequest request, CancellationToken cancellationToken)
        {
            Test test = await _db.Tests.Aggregates().FindByID(request.TestID);
            test.RemoveContributor(request.TeacherID);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
