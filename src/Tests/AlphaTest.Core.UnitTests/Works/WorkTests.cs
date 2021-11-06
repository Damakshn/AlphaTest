using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using AlphaTest.Core.Works;
using AlphaTest.Core.Works.Rules;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Examinations;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Fixtures;
using AlphaTest.Core.UnitTests.Common.Helpers;

namespace AlphaTest.Core.UnitTests.Works
{
    public class WorkTests: UnitTestBase
    {
        [Theory, WorkTestsData]
        public void New_work_cannot_be_started_for_canceled_examination(Test test, Examination examination)
        {
            examination.Cancel();

            AssertBrokenRule<NewWorkCannotBeStartedIfExamIsClosedRule>(() => 
                new Work(test, examination, Guid.NewGuid(), 0));
        }

        [Theory, WorkTestsData]
        public void New_work_cannot_be_started_if_examination_is_already_ended(Test test, Examination examination)
        {
            HelpersForExaminations.SetExaminationDates(examination, DateTime.Now.AddDays(-25), DateTime.Now.AddDays(-10));

            AssertBrokenRule<NewWorkCannotBeStartedIfExaminationIsAreadyEndedRule>(() =>
                new Work(test, examination, Guid.NewGuid(), 0));
        }

        [Theory, WorkTestsData]
        public void New_work_cannot_be_started_if_limit_of_attempts_is_already_exhausted(Test test, Examination examination)
        {
            uint attemptsSpent = (uint)test.AttemptsLimit;
            AssertBrokenRule<NewWorkCannotBeStartedIfAttemptsLimitForTestIsExhaustedRule>(() =>
                new Work(test, examination, Guid.NewGuid(), attemptsSpent));
        }

        [Theory, WorkTestsData]
        public void New_work_can_be_started(Test test, Examination examination)
        {
            Work work = new(test, examination, Guid.NewGuid(), 0);

            Assert.Equal(examination.ID, work.ExaminationID);
            Assert.Equal(DateTime.Now, work.StartedAt, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [WorkTestsMemberData(nameof(TimeLimits))]
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

            Work work = new(test, examination, Guid.NewGuid(), 0);

            Assert.Equal(DateTime.Now + expected, work.ForceEndAt, TimeSpan.FromSeconds(10));
        }


        [Theory, WorkTestsData]
        public void Work_can_be_finished(Work work)
        {
            work.Finish();

            Assert.True(work.IsFinished);
            Assert.Equal(DateTime.Now, (DateTime)work.FinishedAt, TimeSpan.FromSeconds(1));
        }

        [Theory, WorkTestsData]
        public void Work_can_be_finished_only_once(Work work)
        {
            work.Finish();
            AssertBrokenRule<FinishedWorkCannotBeModifiedRule>(() => work.Finish());
        }

        [Theory, WorkTestsData]
        public void Work_can_be_finished_by_force(Work work)
        {
            HelpersForWorks.SetWorkForcedEndDate(work, DateTime.Now);

            work.ForceEnd();

            Assert.True(work.IsFinished);
        }

        [Theory, WorkTestsData]
        public void Work_cannot_be_finished_by_force_if_time_limit_is_not_expired(Work work)
        {
            AssertBrokenRule<ForcedEndMustBeAppliedAtRightTimeRule>(() => work.ForceEnd());
        }

        [Theory, WorkTestsData]
        public void Work_cannot_be_finished_by_force_if_it_is_already_finished(Work work)
        {
            HelpersForWorks.SetWorkForcedEndDate(work, DateTime.Now);

            work.Finish();

            AssertBrokenRule<FinishedWorkCannotBeModifiedRule>(() => work.ForceEnd());
        }

        public static IEnumerable<object[]> TimeLimits =>
            new List<object[]>
            {
                new object[]{ TimeSpan.FromHours(1), TimeSpan.FromDays(1), TimeSpan.FromHours(1)},
                new object[]{ TimeSpan.FromHours(1), TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(30)},
            };
    }
}
