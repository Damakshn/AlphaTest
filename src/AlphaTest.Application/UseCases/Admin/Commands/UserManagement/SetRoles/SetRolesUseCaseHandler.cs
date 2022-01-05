using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MediatR;
using AlphaTest.Core.Users;
using AlphaTest.Application.Exceptions;
using AlphaTest.Application.UseCases.Common;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Application.UseCases.Admin.Commands.UserManagement.SetRoles
{
    public class SetRolesUseCaseHandler : UseCaseHandlerBase<SetRoleUseCaseRequest>
    {
        private UserManager<AlphaTestUser> _userManager;
        private RoleManager<AlphaTestRole> _roleManager;

        public SetRolesUseCaseHandler(AlphaTestContext db, UserManager<AlphaTestUser> userManager, RoleManager<AlphaTestRole> roleManager) : base(db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task<Unit> Handle(SetRoleUseCaseRequest request, CancellationToken cancellationToken)
        {
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            if (request.Roles.Except(allRoles).Any())
            {
                throw new AlphaTestApplicationException(BuildErrorMessageForNonExistingRoles(request.Roles.Except(allRoles).ToList()));
            }


            AlphaTestUser user = await _userManager.FindByIdAsync(request.UserID.ToString());

            var currentRoles = await _userManager.GetRolesAsync(user);
            var addedRoles = request.Roles.Except(currentRoles);
            var removedRoles = currentRoles.Except(request.Roles);
            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
            return Unit.Value;
        }

        private static string  BuildErrorMessageForNonExistingRoles(List<string> extraRoles)
        {   
            if (extraRoles.Count == 1)
                return $"Операция невозможна, роль доступа {extraRoles[0]} не найдена в системе.";
            StringBuilder builder = new();
            builder.Append("Операция невозможна, следующие роли доступа не найдены в системе:\n");
            foreach(var role in extraRoles)
            {
                builder.Append($"{role}");
                if (extraRoles.IndexOf(role) < extraRoles.Count - 1)
                    builder.Append(";\n");
                else
                    builder.Append('.');
            }
            return builder.ToString();
        }
    }
}
