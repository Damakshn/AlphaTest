using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Groups;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Groups.Commands.UnsetCurator
{
    public class UnsetCuratorUseCaseHandler : UseCaseHandlerBase<UnsetCuratorUseCaseRequest>
    {
        public UnsetCuratorUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(UnsetCuratorUseCaseRequest request, CancellationToken cancellationToken)
        {
            Group group = await _db.Groups.Aggregates().FindByID(request.GroupID);
            group.UnsetCurator();
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
