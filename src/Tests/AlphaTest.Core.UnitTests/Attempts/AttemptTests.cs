using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using AlphaTest.Core.Attempts;
using AlphaTest.Core.Attempts.Rules;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Fixtures;
using AlphaTest.Core.UnitTests.Common.Helpers;

namespace AlphaTest.Core.UnitTests.Attempts
{
    public class AttemptTests: UnitTestBase
    {
        [Theory, AttemptTestsData]
        public void New_attempt_cannot_be_started_for_canceled_examination(Test test, Examination examination)
        {
            examination.Cancel();

            AssertBrokenRule<NewAttemptCannotBeStartedIfExamIsClosedRule>(() => 
                new Attempt(test, examination, It.IsAny<int>()));
        }

        [Theory, AttemptTestsData]
        public void New_attempt_cannot_be_started_if_examination_is_already_ended(Test test, Examination examination)
        {
            HelpersForExaminations.SetExaminationDates(examination, DateTime.Now.AddDays(-25), DateTime.Now.AddDays(-10));

            AssertBrokenRule<NewAttemptCannotBeStartedIfExaminationIsAreadyEndedRule>(() =>
                new Attempt(test, examination, It.IsAny<int>()));
        }

        [Theory, AttemptTestsData]
        public void New_attempt_can_be_started(Test test, Examination examination)
        {
            Attempt attempt = new(test, examination, It.IsAny<int>());

            Assert.Equal(examination.ID, attempt.ExaminationID);
            Assert.Equal(DateTime.Now, attempt.StartedAt, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [AttemptTestsMemberData(nameof(TimeLimits))]
        public void Actual_time_limit_is_equal_to_minimum_between_time_limit_for_test_and_remained_time_of_examination(
            TimeSpan timeLimitForTest,
            TimeSpan examTimeRemained,
            TimeSpan expected,
            Test test,
            Examination examination)
        {
            test.ChangeTimeLimit(timeLimitForTest);

            // сроки экзамена приходится ломать через рефлексию, в естественных условиях
            // такая ситуация (когда экзамен вот-вот закончится) возникает сама собой
            HelpersForExaminations.SetExaminationDates(
                examination,
                DateTime.Now,
                DateTime.Now + examTimeRemained
            );

            Attempt attempt = new(test, examination, It.IsAny<int>());

            Assert.Equal(DateTime.Now + expected, attempt.ForceEndAt, TimeSpan.FromSeconds(10));
        }


        [Theory, AttemptTestsData]
        public void Attempt_can_be_finished(Attempt attempt)
        {
            attempt.Finish();

            Assert.True(attempt.IsFinished);
            Assert.Equal(DateTime.Now, (DateTime)attempt.FinishedAt, TimeSpan.FromSeconds(1));
        }

        [Theory, AttemptTestsData]
        public void Attempt_can_be_finished_only_once(Attempt attempt)
        {
            attempt.Finish();
            AssertBrokenRule<FinishedAttemptCannotBeModifiedRule>(() => attempt.Finish());
        }

        [Theory, AttemptTestsData]
        public void Attempt_can_be_finished_by_force(Attempt attempt)
        {
            HelpersForAttempts.SetAttemptForcedEndDate(attempt, DateTime.Now);

            attempt.ForceEnd();

            Assert.True(attempt.IsFinished);
        }

        [Theory, AttemptTestsData]
        public void Attempt_cannot_be_finished_by_force_if_time_limit_is_not_expired(Attempt attempt)
        {
            AssertBrokenRule<ForcedEndMustBeAppliedAtRightTimeRule>(() => attempt.ForceEnd());
        }

        [Theory, AttemptTestsData]
        public void Attempt_cannot_be_finished_by_force_if_it_is_already_finished(Attempt attempt)
        {
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
