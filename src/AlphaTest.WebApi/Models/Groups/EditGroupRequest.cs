using System;

namespace AlphaTest.WebApi.Models.Groups
{
    public class EditGroupRequest
    {
        public string Name { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
