using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAdmin;
using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAuthorOrContributor;

namespace AlphaTest.WebApi.AccessControl.Tests
{
    public class CanViewTestContentsRequirement : IAdminRequirement, IAuthorOrContributorRequirement
    {
    }
}
