using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Users;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.SuspendUser
{
    public class SuspendUserUseCaseHandler : UseCaseHandlerBase<SuspendUserUseCaseRequest>
    {
        public SuspendUserUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(SuspendUserUseCaseRequest request, CancellationToken cancellationToken)
        {
            if (request.CurrentUserID == request.SuspendedUserID)
                throw new AlphaTestApplicationException("Ошибка - невозможно заблокировать самого себя.");
            AlphaTestUser suspendedUser = await _db.Users.Aggregates().FindByID(request.SuspendedUserID);
            suspendedUser.Suspend();
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
