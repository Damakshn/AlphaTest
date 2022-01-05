using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AlphaTest.Core.Tests;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Infrastructure.Database;
using AlphaTest.WebApi.Utils.Security;


namespace AlphaTest.WebApi.AccessControl.SharedRequirements.IsAuthorOrContributor
{
    public class AuthorOrContributorHandler : AuthorizationHandler<IAuthorOrContributorRequirement>
    {
        private readonly AlphaTestContext _db;

        public AuthorOrContributorHandler(AlphaTestContext db)
        {
            _db = db;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorOrContributorRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                string testIdString = httpContext.GetRouteValue("testID").ToString();
                // ToDo throw exception - invalid policy usage
                if (!Guid.TryParse(testIdString, out Guid testID))
                {
                    return;
                }
                Test test = await _db.Tests.Aggregates().FindByID(testID);
                Guid userID = httpContext.User.GetID();
                if ((test.IsAuthor(userID) || test.IsContributor(userID)) && context.User.IsInRole("Teacher"))
                {
                    context.Succeed(requirement);
                }
            }
            return;
            
        }
    }
}
