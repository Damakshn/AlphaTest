using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;
using AlphaTest.Core.Examinations;
using AlphaTest.Infrastructure.Database;
using AlphaTest.Infrastructure.Database.QueryExtensions;
using AlphaTest.WebApi.Utils.Security;

namespace AlphaTest.WebApi.AccessControl.SharedRequirements.IsExaminer
{
    public class ExaminerHandler : AuthorizationHandler<IExaminerRequirement>
    {
        private readonly AlphaTestContext _db;

        public ExaminerHandler(AlphaTestContext db)
        {
            _db = db;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IExaminerRequirement requirement)
        {
            if (context.Resource is HttpContext httpContext)
            {
                string examIdString = httpContext.GetRouteValue("examinationID").ToString();
                // ToDo throw exception - invalid policy usage
                if (!Guid.TryParse(examIdString, out Guid examID))
                {
                    return;
                }
                Examination examination = await _db.Examinations.FindByID(examID);
                Guid userID = httpContext.User.GetID();
                if (examination.ExaminerID == userID)
                {
                    context.Succeed(requirement);
                }
            }
            return;
        }
    }
}
