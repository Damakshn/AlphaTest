using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Groups;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Groups.Commands.DisbandGroup
{
    public class DisbandGroupUseCaseHandler : UseCaseHandlerBase<DisbandGroupUseCaseRequest>
    {
        public DisbandGroupUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(DisbandGroupUseCaseRequest request, CancellationToken cancellationToken)
        {
            Group groupToDisband = await _db.Groups.Aggregates().FindByID(request.GroupID);
            groupToDisband.Disband();
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
