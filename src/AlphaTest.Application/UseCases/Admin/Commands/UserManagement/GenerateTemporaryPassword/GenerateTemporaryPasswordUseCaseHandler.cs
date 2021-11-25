using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Auth.Security;
using AlphaTest.Infrastructure.Auth.UserManagement;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using MediatR;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.GenerateTemporaryPassword
{
    public class GenerateTemporaryPasswordUseCaseHandler : UseCaseHandlerBase<GenerateTemporaryPasswordUseCaseRequest>
    {
        public GenerateTemporaryPasswordUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Unit> Handle(GenerateTemporaryPasswordUseCaseRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _db.Users.Aggregates().FindByID(request.UserID);
            string newPassword = PasswordGenerator.GeneratePassword(SecuritySettings.PasswordOptions);
            user.ResetTemporaryPassword(newPassword);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
