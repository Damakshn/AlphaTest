using AlphaTest.Application.UseCases.Common;
using AlphaTest.Core.Groups;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AlphaTest.Application.UseCases.Groups.DisbandGroup
{
    public class DisbandGroupUseCaseHandler : UseCaseHandlerBase<DisbandGroupUseCaseRequest>
    {
        public DisbandGroupUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(DisbandGroupUseCaseRequest request, CancellationToken cancellationToken)
        {
            Group groupToDisband = await _db.Groups.Aggregates().FindByID(request.GroupID);
            groupToDisband.Disband();
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
