using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Auth;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.CreateUser
{
    public class CreateUserUseCaseHandler : UseCaseHandlerBase<CreateUserUseCaseRequest, Guid>
    {
        public CreateUserUseCaseHandler(AlphaTestContext db) : base(db)
        {
        }

        public override async Task<Guid> Handle(CreateUserUseCaseRequest request, CancellationToken cancellationToken)
        {   
            AppRole initialRole = _db.Roles.FirstOrDefault(r => r.Name == request.InitialRole);

            #region Проверки
            if (initialRole is null)
                throw new AlphaTestApplicationException($"Операция невозможна - роль {request.InitialRole} не найдена");
            if (_db.Users.Any(u => u.Email == request.Email))
                throw new AlphaTestApplicationException($"Невозможно создать учетную запись - email {request.Email} уже занят.");
            #endregion

            AppUser newUser = new(
                request.FirstName,
                request.LastName,
                request.MiddleName,
                request.TemporaryPassword,
                request.Email);
            _db.Users.Add(newUser);
            AppUserRole newUserInRole = new() { Role = initialRole, User = newUser };
            _db.UserRoles.Add(newUserInRole);
            await _db.SaveChangesAsync();
            return newUser.Id;
        }
    }
}
