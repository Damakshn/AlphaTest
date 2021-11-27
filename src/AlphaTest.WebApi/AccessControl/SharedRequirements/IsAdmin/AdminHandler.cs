using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AlphaTest.WebApi.AccessControl.SharedRequirements.IsAdmin
{
    public class AdminHandler : AuthorizationHandler<IAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IAdminRequirement requirement)
        {
            if (context.User.IsInRole("Admin"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
