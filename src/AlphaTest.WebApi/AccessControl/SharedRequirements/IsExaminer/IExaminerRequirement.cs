using Microsoft.AspNetCore.Authorization;

namespace AlphaTest.WebApi.AccessControl.SharedRequirements.IsExaminer
{
    public interface IExaminerRequirement : IAuthorizationRequirement
    {
    }
}
