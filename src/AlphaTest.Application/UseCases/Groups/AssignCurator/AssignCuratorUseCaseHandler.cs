using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Groups;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.Infrastructure.Auth.UserManagement;

namespace AlphaTest.Application.UseCases.Groups.AssignCurator
{
    public class AssignCuratorUseCaseHandler : UseCaseHandlerBase<AssignCuratorUseCaseRequest>
    {
        public AssignCuratorUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(AssignCuratorUseCaseRequest request, CancellationToken cancellationToken)
        {
            AppUser curator = await _db.Users.Aggregates().FindByID(request.CuratorID);
            Group group = await _db.Groups.Aggregates().FindByID(request.GroupID);
            group.AssignCurator(curator);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
