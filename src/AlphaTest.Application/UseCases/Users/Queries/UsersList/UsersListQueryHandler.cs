using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Application.Models.Users;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;

namespace AlphaTest.Application.UseCases.Users.Queries.UsersList
{
    public class UsersListQueryHandler : UseCaseReportingHandlerBase<UsersListQuery, List<UsersListItemDto>>
    {
        public UsersListQueryHandler(AlphaTestContext db) : base(db) { }

        public override async Task<List<UsersListItemDto>> Handle(UsersListQuery request, CancellationToken cancellationToken)
        {
            var query = _db.Users
                .FilterByFIO(request.FIO)
                .FilterByLockStatus(request.IsSuspended)
                .FilterByEmail(request.Email)
                .FilterByRoles(request.Roles, _db)
                .FilterByGroups(request.Groups, _db);

            return await (from user in query
                select new UsersListItemDto()
                {
                    ID = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    Email = user.Email,
                    IsSuspended = user.IsSuspended,
                    Roles = (from userRole in _db.UserRoles
                        join role in _db.Roles on userRole.RoleId equals role.Id
                        where userRole.UserId == user.Id
                        select role.Name).ToList(),
                    Groups = (from g in _db.Groups
                        join m in _db.Memberships on g.ID equals m.GroupID
                        where m.StudentID == user.Id
                        orderby g.BeginDate descending
                        select g.Name).ToList()
                }).ToListAsync();

            
        }

        
    }
}
