using System;
using System.Collections.Generic;
using AlphaTest.Application.Models.Users;
using AlphaTest.Application.UseCases.Common;


namespace AlphaTest.Application.UseCases.Users.Queries.UsersList
{
    public class UsersListQuery : IUseCaseRequest<List<UsersListItemDto>>
    {
        public UsersListQuery(List<string> roles, string fio, bool? isSuspended, string email, List<Guid> groups)
        {
            Roles = roles;
            FIO = fio;
            IsSuspended = isSuspended;
            Email = email;
            Groups = groups;
        }
        
        public List<string> Roles { get; private set; }

        public string FIO { get; private set; }

        public bool? IsSuspended { get; private set; }

        public string Email { get; private set; }

        public List<Guid> Groups { get; private set; }
    }
}
