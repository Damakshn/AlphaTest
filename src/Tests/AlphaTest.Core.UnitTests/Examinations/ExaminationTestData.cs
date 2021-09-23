using System;
using System.Collections.Generic;
using Moq;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Tests;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.UnitTests.Examinations
{
    public class ExaminationTestData
    {
        public ExaminationTestData()
        {
            UserTestData authorData = new() 
            {
                InitialRole = UserRole.TEACHER,
                FirstName = It.IsAny<string>(),
                LastName = It.IsAny<string>(),
                MiddleName = It.IsAny<string>()
            };
            UserTestData contributorData = new()
            {
                InitialRole = UserRole.TEACHER,
                FirstName = It.IsAny<string>(),
                LastName = It.IsAny<string>(),
                MiddleName = It.IsAny<string>()
            };
            TestAuthor = HelpersForUsers.CreateUser(authorData);
            Contributor = HelpersForUsers.CreateUser(contributorData);
            Examiner = TestAuthor;
            // MAYBE перенести в HelpersForTests с возможностью настраивать автора
            Test = new(It.IsAny<string>(), It.IsAny<string>(), TestAuthor.ID, false);
            Test.AddContributor(Contributor);
            HelpersForTests.SetNewStatusForTest(Test, TestStatus.Published);
            Group group1 = new(
                "Первая группа",
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(100),
                false);
            Group group2 = new(
                "Вторая группа",
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(100),
                false);
            Groups = new List<Group>() { group1, group2 };
        }

        public int ID { get; set; }

        public DateTime StartsAt { get; set; } = DateTime.Now.AddDays(1);

        public DateTime EndsAt { get; set; } = DateTime.Now.AddDays(8);

        public User Examiner { get; set; }

        public Test Test { get; set; }

        public User TestAuthor { get; set; }

        public User Contributor { get; set; }

        public List<Group> Groups { get; set; }
    }
}
