using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAdmin;
using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAuthor;


namespace AlphaTest.WebApi.AccessControl.Tests
{
    public class CanSwitchAuthorRequirement : IAuthorOfTestRequirement, IAdminRequirement
    {
    }
}
