using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAdmin;
using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAuthorOrContributor;

namespace AlphaTest.WebApi.AccessControl.CanViewTestContents
{
    public class CanViewTestContentsRequirement : IAdminRequirement, IAuthorOrContributorRequirement
    {
    }
}
