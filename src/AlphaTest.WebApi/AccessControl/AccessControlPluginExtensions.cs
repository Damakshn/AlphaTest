using AlphaTest.WebApi.AccessControl.CanViewTestContents;
using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAdmin;
using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAuthorOrContributor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;


namespace AlphaTest.WebApi.AccessControl
{
    public static class AccessControlPluginExtensions
    {
        public static void AddAccessControlRules(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, AdminHandler>();
            services.AddTransient<IAuthorizationHandler, AuthorOrContributorHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanViewTestContents", policy => policy.Requirements.Add(new CanViewTestContentsRequirement()));
            });
        }
    }
}
