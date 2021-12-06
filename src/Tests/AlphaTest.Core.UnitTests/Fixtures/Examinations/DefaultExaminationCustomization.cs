using AutoFixture;
using System;
using System.Collections.Generic;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Groups;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.UnitTests.Fixtures.FixtureExtensions;

namespace AlphaTest.Core.UnitTests.Fixtures.Examinations
{
    internal class DefaultExaminationCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            DateTime start = DateTime.Now.AddDays(1);
            DateTime end = DateTime.Now.AddDays(10);
            List<Group> noGroups = new();
            fixture.Customize<Examination>(c =>
                c.FromFactory(
                    (Test test) =>
                    {
                        var examiner = fixture.CreateUserMock();
                        examiner.Setup(e => e.IsTeacher).Returns(true);
                        examiner.Setup(e => e.Id).Returns(test.AuthorID);
                        HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
                        var exam = new Examination(test, start, end, examiner.Object, noGroups);
                        HelpersForTests.SetNewStatusForTest(test, TestStatus.Draft);
                        return exam;
                    }
            ));
            fixture.Freeze<Examination>();
        }
    }
}
