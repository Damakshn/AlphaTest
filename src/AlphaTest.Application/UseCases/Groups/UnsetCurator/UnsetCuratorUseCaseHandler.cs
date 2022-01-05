using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Groups;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Groups.UnsetCurator
{
    public class UnsetCuratorUseCaseHandler : UseCaseHandlerBase<UnsetCuratorUseCaseRequest>
    {
        public UnsetCuratorUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(UnsetCuratorUseCaseRequest request, CancellationToken cancellationToken)
        {
            Group group = await _db.Groups.Aggregates().FindByID(request.GroupID);
            group.UnsetCurator();
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
