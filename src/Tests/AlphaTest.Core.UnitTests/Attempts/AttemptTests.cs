using System;
using Xunit;
using Moq;
using AlphaTest.Core.UnitTests.Common;
using System.Collections.Generic;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Attempts.Rules;
using AlphaTest.Core.Users;
using AlphaTest.Core.Groups;

namespace AlphaTest.Core.UnitTests.Attempts
{
    // ToDo требуется масштабный рефакторинг и чистка
    public class AttemptTests: UnitTestBase
    {
        [Fact]
        public void New_attempt_cannot_be_started_for_canceled_examination()
        {
            UserTestData udata = new() { InitialRole = Users.UserRole.TEACHER };
            User author = HelpersForUsers.CreateUser(udata);
            Test test = new(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), author.ID, false);
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Examination examination = new(
                test,
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(10),
                author,
                new List<Group>()
            );

            examination.Cancel();

            AssertBrokenRule<NewAttemptCannotBeStartedIfExamIsClosedRule>(() => 
                new Attempt(It.IsAny<int>(), test, examination, It.IsAny<int>()));
        }

        [Fact]
        public void New_attempt_cannot_be_started_if_examination_is_already_ended()
        {
            UserTestData udata = new() { InitialRole = Users.UserRole.TEACHER };
            User author = HelpersForUsers.CreateUser(udata);
            Test test = new(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), author.ID, false);
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Examination examination = new(
                test,
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(10),
                author,
                new List<Group>()
            );

            HelpersForExaminations.SetExaminationDates(examination, DateTime.Now.AddDays(-25), DateTime.Now.AddDays(-10));

            AssertBrokenRule<NewAttemptCannotBeStartedIfExaminationIsAreadyEndedRule>(() =>
                new Attempt(It.IsAny<int>(), test, examination, It.IsAny<int>()));
        }

        [Fact]
        public void New_attempt_can_be_started()
        {
            UserTestData udata = new() { InitialRole = Users.UserRole.TEACHER };
            User author = HelpersForUsers.CreateUser(udata);
            Test test = new(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), author.ID, false);
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Examination examination = new(
                test,
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(10),
                author,
                new List<Group>()
            );

            Attempt attempt = new(It.IsAny<int>(), test, examination, It.IsAny<int>());

            Assert.Equal(examination.ID, attempt.ExaminationID);
            Assert.Equal(DateTime.Now, attempt.StartedAt, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [MemberData(nameof(TimeLimits))]
        public void Actual_time_limit_is_equal_to_minimum_between_time_limit_for_test_and_remained_time_of_examination(
            TimeSpan timeLimitForTest,
            TimeSpan examTimeRemained,
            TimeSpan expected)
        {
            UserTestData udata = new() { InitialRole = Users.UserRole.TEACHER };
            User author = HelpersForUsers.CreateUser(udata);
            Test test = new(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), author.ID, false);
            test.ChangeTimeLimit(timeLimitForTest);
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Examination examination = new(
                test,
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(10),
                author,
                new List<Group>()
            );
            
            HelpersForExaminations.SetExaminationDates(
                examination,
                DateTime.Now,
                DateTime.Now + examTimeRemained
            );

            Attempt attempt = new(It.IsAny<int>(), test, examination, It.IsAny<int>());

            Assert.Equal(DateTime.Now + expected, attempt.ForceEndAt, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void Attempt_can_be_finished()
        {
            UserTestData udata = new() { InitialRole = Users.UserRole.TEACHER };
            User author = HelpersForUsers.CreateUser(udata);
            Test test = new(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), author.ID, false);
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Examination examination = new(
                test,
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(10),
                author,
                new List<Group>()
            );

            Attempt attempt = new(It.IsAny<int>(), test, examination, It.IsAny<int>());
            attempt.Finish();
            Assert.True(attempt.IsFinished);
            Assert.Equal(DateTime.Now, (DateTime)attempt.FinishedAt, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void Attempt_can_be_finished_only_once()
        {
            UserTestData udata = new() { InitialRole = Users.UserRole.TEACHER };
            User author = HelpersForUsers.CreateUser(udata);
            Test test = new(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), author.ID, false);
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Examination examination = new(
                test,
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(10),
                author,
                new List<Group>()
            );

            Attempt attempt = new(It.IsAny<int>(), test, examination, It.IsAny<int>());
            attempt.Finish();

            AssertBrokenRule<FinishedAttemptCannotBeModifiedRule>(() => attempt.Finish());
        }

        [Fact]
        public void Attempt_can_be_finished_by_force()
        {
            UserTestData udata = new() { InitialRole = Users.UserRole.TEACHER };
            User author = HelpersForUsers.CreateUser(udata);
            Test test = new(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), author.ID, false);
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Examination examination = new(
                test,
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(10),
                author,
                new List<Group>()
            );
            Attempt attempt = new(It.IsAny<int>(), test, examination, It.IsAny<int>());
            HelpersForAttempts.SetAttemptForcedEndDate(attempt, DateTime.Now);

            attempt.ForceEnd();

            Assert.True(attempt.IsFinished);
            
        }

        [Fact]
        public void Attempt_cannot_be_finished_by_force_if_time_limit_is_not_expired()
        {
            UserTestData udata = new() { InitialRole = Users.UserRole.TEACHER };
            User author = HelpersForUsers.CreateUser(udata);
            Test test = new(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), author.ID, false);
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Examination examination = new(
                test,
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(10),
                author,
                new List<Group>()
            );
            Attempt attempt = new(It.IsAny<int>(), test, examination, It.IsAny<int>());

            AssertBrokenRule<ForcedEndMustBeAppliedAtRightTimeRule>(() => attempt.ForceEnd());
        }

        [Fact]
        public void Attempt_cannot_be_finished_by_force_if_it_is_already_finished()
        {
            UserTestData udata = new() { InitialRole = Users.UserRole.TEACHER };
            User author = HelpersForUsers.CreateUser(udata);
            Test test = new(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), author.ID, false);
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Examination examination = new(
                test,
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(10),
                author,
                new List<Group>()
            );
            Attempt attempt = new(It.IsAny<int>(), test, examination, It.IsAny<int>());
            HelpersForAttempts.SetAttemptForcedEndDate(attempt, DateTime.Now);
            attempt.Finish();

            AssertBrokenRule<FinishedAttemptCannotBeModifiedRule>(() => attempt.ForceEnd());
        }

        public static IEnumerable<object[]> TimeLimits =>
            new List<object[]>
            {
                new object[]{ TimeSpan.FromHours(1), TimeSpan.FromDays(1), TimeSpan.FromHours(1)},
                new object[]{ TimeSpan.FromHours(1), TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(30)},
            };
    }
}
