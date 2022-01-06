using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Auth.Security;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.GenerateTemporaryPassword
{
    public class GenerateTemporaryPasswordUseCaseHandler : UseCaseHandlerBase<GenerateTemporaryPasswordUseCaseRequest>
    {
        public GenerateTemporaryPasswordUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(GenerateTemporaryPasswordUseCaseRequest request, CancellationToken cancellationToken)
        {
            AlphaTestUser user = await _db.Users.Aggregates().FindByID(request.UserID);
            string newPassword = PasswordGenerator.GeneratePassword(SecuritySettings.PasswordOptions);
            user.ResetTemporaryPassword(newPassword);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
