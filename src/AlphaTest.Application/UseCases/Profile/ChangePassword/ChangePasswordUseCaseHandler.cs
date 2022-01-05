using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MediatR;
using AlphaTest.Core.Users;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Profile.ChangePassword
{
    public class ChangePasswordUseCaseHandler : UseCaseHandlerBase<ChangePasswordUseCaseRequest>
    {   
        private readonly UserManager<AlphaTestUser> _userManager;

        public ChangePasswordUseCaseHandler(AlphaTestContext db, UserManager<AlphaTestUser> userManager) : base(db)
        {   
            _userManager = userManager;
        }

        public override async Task<Unit> Handle(ChangePasswordUseCaseRequest request, CancellationToken cancellationToken)
        {
            AlphaTestUser user = await _db.Users.Aggregates().FindByID(request.UserID);
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
