using Microsoft.AspNetCore.Authorization;


namespace AlphaTest.WebApi.AccessControl.SharedRequirements.IsAuthor
{
    public interface IAuthorOfTestRequirement : IAuthorizationRequirement
    {
    }
}
