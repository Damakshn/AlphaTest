using AlphaTest.WebApi.AccessControl.SharedRequirements.IsAdmin;
using AlphaTest.WebApi.AccessControl.SharedRequirements.IsExaminer;

namespace AlphaTest.WebApi.AccessControl.Schedule
{
    public class CanEditScheduleRequirement : IAdminRequirement, IExaminerRequirement
    {
    }
}
