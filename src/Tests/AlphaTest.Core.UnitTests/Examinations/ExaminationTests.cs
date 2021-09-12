using System;
using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.Examinations.Rules;
using System.Collections.Generic;
using AlphaTest.Core.Groups;
using AlphaTest.Core.Users;


namespace AlphaTest.Core.UnitTests.Examinations
{
    public class ExaminationTests: UnitTestBase
    {
        [Fact]
        public void Examination_can_be_created_only_for_published_test()
        {
            ExaminationTestData data = new();

            HelpersForTests.SetNewStatusForTest(data.Test, TestStatus.Draft);

            AssertBrokenRule<ExaminationsCanBeCreatedOnlyForPublishedTestsRule>(() =>
                new Examination(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups)
            );
        }

        [Fact]
        public void Start_of_examination_must_be_earlier_than_end()
        {
            DateTime start = DateTime.Now.AddDays(1);
            DateTime end = start - TimeSpan.FromDays(10);
            ExaminationTestData data = new() { StartsAt = start, EndsAt = end };
            
            AssertBrokenRule<StartOfExaminationMustBeEarlierThanEndRule>(() =>
                new Examination(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups)
            );
        }

        [Fact]
        public void Examination_cannot_start_in_the_past()
        {
            DateTime start = DateTime.Now - new TimeSpan(5, 0, 0);
            DateTime end = DateTime.Now.AddDays(1);
            ExaminationTestData data = new() { StartsAt = start, EndsAt = end };
            
            AssertBrokenRule<ExaminationCannotBeCreatedThePastRule>(() =>
                new Examination(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups)
            );
        }

        [Fact]
        public void Examination_duration_cannot_be_shorter_than_time_limit_for_test()
        {
            DateTime start = DateTime.Now.AddDays(1);
            DateTime end = start + new TimeSpan(0, 30, 0);
            ExaminationTestData data = new() { StartsAt = start, EndsAt = end };
            // иначе тест не даст поменять лимит времени
            HelpersForTests.SetNewStatusForTest(data.Test, TestStatus.Draft);
            data.Test.ChangeTimeLimit(new TimeSpan(2, 0, 0));
            HelpersForTests.SetNewStatusForTest(data.Test, TestStatus.Published);
            
            AssertBrokenRule<ExamDurationCannotBeShorterThanTimeLimitInTestRule>(() =>
                new Examination(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups)
            );
        }

        [Theory]
        [MemberData(nameof(HelpersForUsers.NonTeacherRoles), MemberType = typeof(HelpersForUsers))]
        public void Examiner_must_be_teacher(UserRole role)
        {
            UserTestData examinerData = new() { ID = 1001, InitialRole = role };
            User examiner = HelpersForUsers.CreateUser(examinerData);
            ExaminationTestData data = new() { Examiner = examiner };

            AssertBrokenRule<ExaminerMustBeTeacherRule>(() =>
                new Examination(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups)
            );
        }

        [Fact]
        public void Examiner_must_be_author_or_contributor_of_the_test()
        {
            UserTestData examinerData = new() { ID = 1001, InitialRole = UserRole.TEACHER };
            User examiner = HelpersForUsers.CreateUser(examinerData);
            ExaminationTestData data = new() { Examiner = examiner };

            AssertBrokenRule<ExaminerMustBeAuthorOrContributorOfTheTestRule>(() =>
                new Examination(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups)
            );
        }
    }
}
