using System.Collections.Generic;

namespace AlphaTest.WebApi.Models.Admin.UserManagement
{
    public class SetRolesRequest
    {
        public List<string> Roles { get; set; }
    }
}
