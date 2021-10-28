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
            AppUser newUser = new(
                request.FirstName,
                request.LastName,
                request.MiddleName,
                request.TemporaryPassword,
                request.Email);
            AppRole initialRole = _db.Roles.FirstOrDefault(r => r.Name == request.InitialRole);
            if (initialRole is null)
                throw new AlphaTestApplicationException($"Операция невозможна - роль {request.InitialRole} не найдена");
            AppUserRole newUserInRole = new() { Role = initialRole, User = newUser };
            _db.Users.Add(newUser);
            _db.UserRoles.Add(newUserInRole);
            // ToDo check if user exists
            await _db.SaveChangesAsync();
            return newUser.Id;
        }
    }
}
