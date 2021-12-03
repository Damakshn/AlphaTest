using AlphaTest.WebApi.AccessControl.Tests;
using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAdmin;
using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAuthorOrContributor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAuthor;

namespace AlphaTest.WebApi.AccessControl
{
    public static class AccessControlPluginExtensions
    {
        public static void AddAccessControlRules(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, AdminHandler>();
            services.AddTransient<IAuthorizationHandler, AuthorOrContributorHandler>();
            services.AddTransient<IAuthorizationHandler, AuthorOfTestHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "CanViewTestContents", 
                    policy => policy.Requirements.Add(new CanViewTestContentsRequirement()));
                options.AddPolicy(
                    "CanEditTest",
                    policy => policy.Requirements.Add(new CanEditTestRequirement()));
                options.AddPolicy(
                    "AuthorOnly",
                    policy => policy.Requirements.Add(new AuthorRequirement()));
                options.AddPolicy(
                    "CanSwitchAuthor",
                    policy => policy.Requirements.Add(new CanSwitchAuthorRequirement()));
            });
        }
    }
}
