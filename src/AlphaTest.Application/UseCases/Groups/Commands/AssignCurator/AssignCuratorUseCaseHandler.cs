using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Groups.Commands.AssignCurator
{
    public class AssignCuratorUseCaseHandler : UseCaseHandlerBase<AssignCuratorUseCaseRequest>
    {
        public AssignCuratorUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(AssignCuratorUseCaseRequest request, CancellationToken cancellationToken)
        {
            AlphaTestUser curator = await _db.Users.Aggregates().FindByID(request.CuratorID);
            Group group = await _db.Groups.Aggregates().FindByID(request.GroupID);
            group.AssignCurator(curator);
            await _db.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
