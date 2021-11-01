using System;
using System.Collections.Generic;
using AlphaTest.Application.UseCases.Common;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.SetRoles
{
    public class SetRoleUseCaseRequest : IUseCaseRequest
    {
        public SetRoleUseCaseRequest(Guid userID, List<string> roles)
        {
            UserID = userID;
            Roles = roles;
        }

        public Guid UserID { get; private set; }

        public List<string> Roles { get; private set; }
    }
}
