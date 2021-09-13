using System;

namespace AlphaTest.Core.UnitTests.Common
{
    public class GroupTestData
    {
        public GroupTestData(){ }

        public int ID { get; set; }

        public string Name { get; set; } = "НазваниеЧтобыБыло";

        public DateTime BeginDate { get; set; } = DateTime.Now.AddDays(1);

        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(100);

        public bool GroupAlreadyExists { get; set; } = false;
    }
}
