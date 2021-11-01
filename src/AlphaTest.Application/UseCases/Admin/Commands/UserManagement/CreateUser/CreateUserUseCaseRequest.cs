using System;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.CreateUser
{
    public class CreateUserUseCaseRequest : IUseCaseRequest<Guid>
    {
        public CreateUserUseCaseRequest(
            string firstName,
            string lastName,
            string middleName,
            string email,
            string temporaryPassword,
            string initialRole)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Email = email;
            TemporaryPassword = temporaryPassword;
            InitialRole = initialRole;
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string MiddleName { get; private set; }

        public string Email { get; private set; }

        public string TemporaryPassword { get; private set; }

        public string InitialRole { get; private set; }

    }
}
