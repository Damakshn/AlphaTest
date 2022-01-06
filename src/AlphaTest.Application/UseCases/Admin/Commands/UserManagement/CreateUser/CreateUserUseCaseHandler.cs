using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Core.Users;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.DataAccess.EF.Abstractions;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.CreateUser
{
    public class CreateUserUseCaseHandler : UseCaseHandlerBase<CreateUserUseCaseRequest, Guid>
    {
        public CreateUserUseCaseHandler(IDbContext db) : base(db)
        {
        }

        public override async Task<Guid> Handle(CreateUserUseCaseRequest request, CancellationToken cancellationToken)
        {   
            AlphaTestRole initialRole = _db.Roles.FirstOrDefault(r => r.Name == request.InitialRole);

            #region Проверки
            if (initialRole is null)
                throw new AlphaTestApplicationException($"Операция невозможна - роль {request.InitialRole} не найдена");
            if (_db.Users.Any(u => u.Email == request.Email))
                throw new AlphaTestApplicationException($"Невозможно создать учетную запись - email {request.Email} уже занят.");
            #endregion

            AlphaTestUser newUser = new(
                request.FirstName,
                request.LastName,
                request.MiddleName,
                request.TemporaryPassword,
                request.Email);
            _db.Users.Add(newUser);
            AlphaTestUserRole newUserInRole = new() { Role = initialRole, User = newUser };
            _db.UserRoles.Add(newUserInRole);
            await _db.SaveChangesAsync(cancellationToken);
            return newUser.Id;
        }
    }
}
