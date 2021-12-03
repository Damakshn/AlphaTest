using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using AlphaTest.Core.Tests;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.WebApi.Utils.Security;

namespace AlphaTest.WebApi.AccessControl.SharedRequirements.IsAuthor
{
    public class AuthorOfTestHandler : AuthorizationHandler<IAuthorOfTestRequirement>
    {
        private readonly AlphaTestContext _db;

        public AuthorOfTestHandler(AlphaTestContext db)
        {
            _db = db;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorOfTestRequirement requirement)
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
                if (test.IsAuthor(userID) && context.User.IsInRole("Teacher"))
                {
                    context.Succeed(requirement);
                }
            }
            return;
        }
    }
}
