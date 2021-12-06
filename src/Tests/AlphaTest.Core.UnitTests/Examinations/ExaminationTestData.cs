using System;
using System.Collections.Generic;
using Moq;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Tests;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.UnitTests.Examinations
{
    public class ExaminationTestData
    {
        public ExaminationTestData()
        {   
            Mock<IAlphaTestUser> createTeacher() 
            {
                var mockedTeacher = new Mock<IAlphaTestUser>();
                mockedTeacher.Setup(m => m.Id).Returns(Guid.NewGuid());
                mockedTeacher.Setup(m => m.IsTeacher).Returns(true);
                mockedTeacher.Setup(m => m.IsSuspended).Returns(false);
                return mockedTeacher;
            };

            TestAuthor = createTeacher().Object;
            Contributor = createTeacher().Object;
            Examiner = TestAuthor;
            // MAYBE перенести в HelpersForTests с возможностью настраивать автора
            Test = new(It.IsAny<string>(), It.IsAny<string>(), TestAuthor.Id, false);
            Test.AddContributor(Contributor);
            HelpersForTests.SetNewStatusForTest(Test, TestStatus.Published);
            Group group1 = new(
                "Первая группа",
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(100),
                null,
                false);
            Group group2 = new(
                "Вторая группа",
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(100),
                null,
                false);
            Groups = new List<Group>() { group1, group2 };
        }

        public DateTime StartsAt { get; set; } = DateTime.Now.AddDays(1);

        public DateTime EndsAt { get; set; } = DateTime.Now.AddDays(8);

        public IAlphaTestUser Examiner { get; set; }

        public Test Test { get; set; }

        public IAlphaTestUser TestAuthor { get; set; }

        public IAlphaTestUser Contributor { get; set; }

        public List<Group> Groups { get; set; }
    }
}
