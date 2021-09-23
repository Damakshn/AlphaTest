using AutoFixture;
using System;
using System.Collections.Generic;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Users;
using AlphaTest.Core.Groups;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.TestingHelpers;

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
                    (Test test, User examiner) =>
                    {
                        HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
                        EntityIDSetter.SetIDTo(examiner, test.AuthorID);
                        return new Examination(test, start, end, examiner, noGroups);
                    }
                        
            ));
        }
    }
}
