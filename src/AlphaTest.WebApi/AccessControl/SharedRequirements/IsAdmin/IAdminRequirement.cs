using Microsoft.AspNetCore.Authorization;

namespace AlphaTest.WebApi.AccessControl.SharedRequirements.IsAdmin
{
    public interface IAdminRequirement : IAuthorizationRequirement
    {
    }
}
