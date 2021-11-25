using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using Microsoft.AspNetCore.Identity;
using AlphaTest.Application.Exceptions;
using AlphaTest.Infrastructure.Auth.UserManagement;

namespace AlphaTest.Application.UseCases.Profile.ChangePassword
{
    public class ChangePasswordUseCaseHandler : UseCaseHandlerBase<ChangePasswordUseCaseRequest>
    {   
        private readonly UserManager<AppUser> _userManager;

        public ChangePasswordUseCaseHandler(AlphaTestContext db, UserManager<AppUser> userManager) : base(db)
        {   
            _userManager = userManager;
        }

        public override async Task<Unit> Handle(ChangePasswordUseCaseRequest request, CancellationToken cancellationToken)
        {   
            AppUser user = await _db.Users.Aggregates().FindByID(request.UserID);
            // ToDo check validator exists
            var validationResult = await _userManager.PasswordValidators[0].ValidateAsync(_userManager, user, request.NewPassword);
            if (validationResult.Succeeded == false)
            {
                // ToDo include validation errors
                throw new AlphaTestApplicationException("Пароль не соответствует требованиям безопасности.");
            }
            user.ChangePassword(request.OldPassword, request.NewPassword, request.NewPasswordRepeat);
            _db.SaveChanges();
            return Unit.Value;
        }
    }
}
