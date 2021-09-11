using System;
using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Examinations.Rules;
using System.Collections.Generic;
using AlphaTest.Core.Groups;

namespace AlphaTest.Core.UnitTests.Examinations
{
    public class ExaminationTests: UnitTestBase
    {
        /*
        ToDo
            class ExamTestData
                StartsAt
                EndsAt
                Test
                    author
                    contributors
                
        */
        [Fact]
        public void Examination_can_be_created_only_for_published_test()
        {
            Test test = GetTestForExamination();
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Draft);
            DateTime start = DateTime.Now.AddDays(1);
            DateTime end = start.AddDays(1);
            AssertBrokenRule<ExaminationsCanBeCreatedOnlyForPublishedTestsRule>(() =>
                new Examination(test, start, end, null, new List<Group>())
            );
        }

        [Fact]
        public void Start_of_examination_must_be_earlier_than_end()
        {
            Test test = GetTestForExamination();
            DateTime end = DateTime.Now.AddDays(1);
            DateTime start = end.AddDays(1);
            AssertBrokenRule<StartOfExaminationMustBeEarlierThanEndRule>(() =>
                new Examination(test, start, end, null, new List<Group>())
            );
        }

        [Fact]
        public void Examination_cannot_start_in_the_past()
        {
            Test test = GetTestForExamination();
            DateTime start = DateTime.Now - new TimeSpan(5, 0, 0);
            DateTime end = DateTime.Now.AddDays(1);
            AssertBrokenRule<ExaminationCannotBeCreatedThePastRule>(() =>
                new Examination(test, start, end, null, new List<Group>())
            );
        }

        [Fact]
        public void Examination_duration_cannot_be_shorter_than_time_limit_for_test()
        {
            Test test = GetTestForExamination();
            // иначе тест не даст поменять лимит времени
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Draft);
            test.ChangeTimeLimit(new TimeSpan(2, 0, 0));
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            DateTime start = DateTime.Now.AddDays(1);
            DateTime end = start + new TimeSpan(0, 30, 0);
            AssertBrokenRule<ExamDurationCannotBeShorterThanTimeLimitInTestRule>(() =>
                new Examination(test, start, end, null, new List<Group>())
            );
        }

        [Fact]
        public void Examiner_must_be_teacher()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Examiner_must_be_author_or_contributor_of_the_test()
        {
            throw new NotImplementedException();
        }

        private static Test GetTestForExamination()
        {
            Test test = HelpersForTests.GetDefaultTest();
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            return test;
        }


    }
}
