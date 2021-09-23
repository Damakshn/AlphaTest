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
using System.Linq;

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
            UserTestData examinerData = new() { InitialRole = role };
            User examiner = HelpersForUsers.CreateUser(examinerData);
            ExaminationTestData data = new() { Examiner = examiner };

            AssertBrokenRule<ExaminerMustBeTeacherRule>(() =>
                new Examination(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups)
            );
        }

        [Fact]
        public void Examiner_must_be_author_or_contributor_of_the_test()
        {
            UserTestData examinerData = new() { InitialRole = UserRole.TEACHER };
            User examiner = HelpersForUsers.CreateUser(examinerData);
            ExaminationTestData data = new() { Examiner = examiner };

            AssertBrokenRule<ExaminerMustBeAuthorOrContributorOfTheTestRule>(() =>
                new Examination(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups)
            );
        }

        [Fact]
        public void Examination_dates_can_be_changed()
        {
            ExaminationTestData data = new() 
            { 
                StartsAt = DateTime.Now.AddDays(1),
                EndsAt = DateTime.Now.AddDays(10) 
            };
            Examination exam = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            DateTime newStart = exam.StartsAt.AddDays(1);
            DateTime newEnd = exam.EndsAt.AddDays(-1);

            exam.ChangeDates(newStart, newEnd, data.Test);

            Assert.Equal(newStart, exam.StartsAt);
            Assert.Equal(newEnd, exam.EndsAt);
        }

        [Fact]
        public void New_exam_dates_cannot_be_set_backwards()
        {
            ExaminationTestData data = new();
            Examination exam = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            DateTime newStart = DateTime.Now.AddDays(10);
            DateTime newEnd = exam.EndsAt.AddDays(1);

            AssertBrokenRule<StartOfExaminationMustBeEarlierThanEndRule>(() =>
                exam.ChangeDates(newStart, newEnd, data.Test)
            );
        }

        [Fact]
        public void Start_of_examination_cannot_be_moved_if_examination_already_started()
        {
            ExaminationTestData data = new()
            {
                StartsAt = DateTime.Now.AddDays(1),
                EndsAt = DateTime.Now.AddDays(10)
            };
            Examination exam = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            DateTime newStart = DateTime.Now.AddDays(1);
            // с помощью рефлекссии подменяем дату начала экзамена
            HelpersForExaminations.SetExaminationDates(exam, DateTime.Now.AddHours(-1), exam.EndsAt);

            AssertBrokenRule<StartOfExamCannotBeMovedIfExamAlreadyStartedRule>(() =>
                exam.ChangeDates(newStart, data.EndsAt, data.Test)
            );
        }

        [Fact]
        public void Examination_cannot_be_moved_into_the_past()
        {
            ExaminationTestData data = new()
            {
                StartsAt = DateTime.Now.AddDays(1),
                EndsAt = DateTime.Now.AddDays(10)
            };
            Examination exam = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            DateTime newStart = DateTime.Now.AddDays(-2);

            AssertBrokenRule<ExaminationCannotBeCreatedThePastRule>(() =>
                exam.ChangeDates(newStart, data.EndsAt, data.Test)
            );
        }

        [Fact]
        public void Exam_dates_cannnot_be_changed_to_make_exam_duration_shorter_than_time_limit_for_test()
        {
            int hours_for_test = 2;
            int hours_for_exam = 1;
            ExaminationTestData data = new()
            {
                StartsAt = DateTime.Now.AddDays(1),
                EndsAt = DateTime.Now.AddDays(10)
            };
            HelpersForTests.SetNewStatusForTest(data.Test, TestStatus.Draft);
            data.Test.ChangeTimeLimit(TimeSpan.FromHours(hours_for_test));
            HelpersForTests.SetNewStatusForTest(data.Test, TestStatus.Published);
            Examination exam = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            DateTime newEnd = data.StartsAt.AddHours(hours_for_exam);

            AssertBrokenRule<ExamDurationCannotBeShorterThanTimeLimitInTestRule>(() =>
                exam.ChangeDates(data.StartsAt, newEnd, data.Test)
            );
        }

        [Fact]
        public void Examination_can_be_canceled()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            examination.Cancel();

            Assert.True(examination.IsCanceled);
        }

        [Fact]
        public void Examination_can_be_canceled_after_start()
        {
            ExaminationTestData data = new()
            {
                StartsAt = DateTime.Now.AddDays(1),
                EndsAt = DateTime.Now.AddDays(10)
            };
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);
            // с помощью рефлексии сдвигаем начало экзамена в прошлое
            HelpersForExaminations.SetExaminationDates(examination, DateTime.Now.AddMinutes(-1), examination.EndsAt);
            examination.Cancel();

            Assert.True(examination.IsCanceled);
        }

        [Theory]
        [MemberData(nameof(HelpersForUsers.NonTeacherRoles), MemberType = typeof(HelpersForUsers))]
        public void Examiner_cannot_be_switched_if_he_is_not_teacher(UserRole role)
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);
            UserTestData userData = new(){InitialRole = role};
            User examiner = HelpersForUsers.CreateUser(userData);

            AssertBrokenRule<ExaminerMustBeTeacherRule>(() =>
                examination.SwitchExaminer(examiner, data.Test)
            );

        }

        [Fact]
        public void New_examiner_must_be_author_or_contributor()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);
            UserTestData userData = new(){ InitialRole = UserRole.TEACHER };
            User examiner = HelpersForUsers.CreateUser(userData);

            AssertBrokenRule<ExaminerMustBeAuthorOrContributorOfTheTestRule>(() =>
                examination.SwitchExaminer(examiner, data.Test)
            );
        }

        [Fact]
        public void Author_of_test_can_become_new_examiner()
        {
            ExaminationTestData data = new();
            data.Examiner = data.Contributor;
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            examination.SwitchExaminer(data.TestAuthor, data.Test);

            Assert.Equal(data.TestAuthor.ID, examination.ExaminerID);
        }

        [Fact]
        public void Contributor_can_become_new_examiner()
        {
            ExaminationTestData data = new();
            data.Examiner = data.TestAuthor;
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            examination.SwitchExaminer(data.Contributor, data.Test);

            Assert.Equal(data.Contributor.ID, examination.ExaminerID);
        }

        [Fact]
        public void Disbanded_group_cannot_participate_the_examination()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            Group oneMoreGroup = HelpersForGroups.CreateGroup(new GroupTestData());
            oneMoreGroup.Disband();

            AssertBrokenRule<DisbandedOrInactiveGroupsCannotParticipateExamRule>(() =>
                examination.AddGroup(oneMoreGroup)
            );
        }

        [Fact]
        public void Outdated_group_cannot_participate_the_examination()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            Group oneMoreGroup = HelpersForGroups.CreateGroup(new GroupTestData());
            // подменяем срок существования группы как будто она уже давно не активна
            HelpersForGroups.SetGroupDates(oneMoreGroup, DateTime.Now.AddDays(-365), DateTime.Now.AddDays(-100));

            AssertBrokenRule<DisbandedOrInactiveGroupsCannotParticipateExamRule>(() =>
                examination.AddGroup(oneMoreGroup)
            );
        }

        [Fact]
        public void Group_can_be_added_to_exam_only_once()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            Group oneMoreGroup = HelpersForGroups.CreateGroup(new GroupTestData());
            examination.AddGroup(oneMoreGroup);

            AssertBrokenRule<GroupCanBeAddedToExamOnlyOnceRule>(() =>
                examination.AddGroup(oneMoreGroup)
            );
        }
        
        [Fact]
        public void Group_can_be_added_to_exam_after_creation()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            Group oneMoreGroup = HelpersForGroups.CreateGroup(new GroupTestData());
            examination.AddGroup(oneMoreGroup);
            Assert.Equal(1, examination.Participations.Count(p => p.GroupID == oneMoreGroup.ID));
        }

        [Fact]
        public void Multiple_groups_cannot_be_added_if_at_least_one_of_them_is_disbanded()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            List<Group> groups = new()
            {
                HelpersForGroups.CreateGroup(new GroupTestData()),
                HelpersForGroups.CreateGroup(new GroupTestData()),
                HelpersForGroups.CreateGroup(new GroupTestData())
            };
            // подменяем срок существования группы как будто она уже давно не активна
            groups[1].Disband();

            AssertBrokenRule<DisbandedOrInactiveGroupsCannotParticipateExamRule>(() =>
                examination.AddGroups(groups)
            );
        }

        [Fact]
        public void Multiple_groups_cannot_be_added_if_at_least_one_of_them_is_outdated()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            List<Group> groups = new()
            {
                HelpersForGroups.CreateGroup(new GroupTestData()),
                HelpersForGroups.CreateGroup(new GroupTestData()),
                HelpersForGroups.CreateGroup(new GroupTestData())
            };
            HelpersForGroups.SetGroupDates(groups[0], DateTime.Now.AddDays(-365), DateTime.Now.AddDays(-100));

            AssertBrokenRule<DisbandedOrInactiveGroupsCannotParticipateExamRule>(() =>
                examination.AddGroups(groups)
            );
        }

        [Fact]
        public void Multiple_groups_cannot_be_added_to_exam_if_at_least_one_of_them_is_already_in_list()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            List<Group> groups = new()
            {
                HelpersForGroups.CreateGroup(new GroupTestData()),
                HelpersForGroups.CreateGroup(new GroupTestData()),
                data.Groups[0]
            };

            AssertBrokenRule<GroupCanBeAddedToExamOnlyOnceRule>(() =>
                examination.AddGroups(groups)
            );
        }

        [Fact]
        public void Multiple_groups_can_be_added_to_exam_after_creation()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            List<Group> groups = new()
            {
                HelpersForGroups.CreateGroup(new GroupTestData()),
                HelpersForGroups.CreateGroup(new GroupTestData()),
                HelpersForGroups.CreateGroup(new GroupTestData())
            };
            Guid[] ids = groups.Select(g => g.ID).ToArray();

            examination.AddGroups(groups);

            Assert.Equal(3, examination.Participations.Count(p => ids.Contains(p.GroupID)));
        }

        [Fact]
        public void Group_cannot_be_removed_from_exam_if_it_not_participating()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);
            Group oneMoreGroup = HelpersForGroups.CreateGroup(new GroupTestData());

            AssertBrokenRule<NonParticipatingGroupsCannotBeExcludedFromExamRule>(() =>
                examination.RemoveGroup(oneMoreGroup)
            );
        }

        [Fact]
        public void Group_can_be_exluded_from_exam()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            examination.RemoveGroup(data.Groups[0]);

            Assert.Equal(0, examination.Participations.Count(p => p.GroupID == data.Groups[0].ID));
        }

        [Fact]
        public void Multiple_groups_cannot_be_excluded_from_exam_if_at_least_one_of_them_is_not_participating()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);
            List<Group> groups = new()
            {
                HelpersForGroups.CreateGroup(new GroupTestData()),
                data.Groups[0],
                data.Groups[1]
            };

            AssertBrokenRule<NonParticipatingGroupsCannotBeExcludedFromExamRule>(() =>
                examination.RemoveGroups(groups)
            );
        }

        [Fact]
        public void Multiple_groups_can_be_excluded_from_exam()
        {
            ExaminationTestData data = new();
            Examination examination = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);
            List<Group> groups = new()
            {
                data.Groups[0],
                data.Groups[1]
            };

            examination.RemoveGroups(groups);

            Assert.Equal(0, examination.Participations.Count);
        }

        [Theory]
        [MemberData(nameof(ExamEditingDelegates))]
        public void Exam_cannot_be_modified_if_it_is_already_ended(Action<ExaminationTestData, Examination> examEditingDelegate)
        {
            ExaminationTestData data = new();
            Examination exam = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            var pastBegin = DateTime.Now.AddDays(-15);
            var pastEnd = DateTime.Now.AddDays(-10);

            // через рефлексию уносим экзамен в прошлое
            HelpersForExaminations.SetExaminationDates(exam, pastBegin, pastEnd);

            AssertBrokenRule<EndedExamCannotBeModifiedRule>(() =>
                examEditingDelegate(data,exam)
            );
        }

        [Theory]
        [MemberData(nameof(ExamEditingDelegates))]
        public void Exam_cannot_be_modified_if_it_is_canceled(Action<ExaminationTestData, Examination> examEditingDelegate)
        {
            ExaminationTestData data = new();
            Examination exam = new(data.Test, data.StartsAt, data.EndsAt, data.Examiner, data.Groups);

            exam.Cancel();

            AssertBrokenRule<CanceledExaminationCannotBeModifiedRule>(() =>
                examEditingDelegate(data, exam)
            );
        }

        public static IEnumerable<object[]> ExamEditingDelegates =>
            new List<object[]>
            {
                new object[]{CancelExamination},
                new object[]{ChangeExaminationDates},
                new object[]{SwitchExaminer},
                new object[]{AddGroup},
                new object[]{AddGroups},
                new object[]{RemoveGroup},
                new object[]{RemoveGroups}
            };

        private static Action<ExaminationTestData, Examination> CancelExamination = (data, exam) => exam.Cancel();

        private static Action<ExaminationTestData, Examination> ChangeExaminationDates = (data, exam) =>
            exam.ChangeDates(exam.StartsAt.AddDays(1), exam.EndsAt.AddDays(1), data.Test);

        private static Action<ExaminationTestData, Examination> SwitchExaminer = (data, exam) => 
            exam.SwitchExaminer(data.Contributor, data.Test);

        private static Action<ExaminationTestData, Examination> AddGroup = (data, exam) => 
            exam.AddGroup(
                HelpersForGroups.CreateGroup(
                    new GroupTestData()
                    )
                );

        private static Action<ExaminationTestData, Examination> AddGroups = (data, exam) =>
            exam.AddGroups(
                new List<Group>
                {
                    HelpersForGroups.CreateGroup(new()),
                    HelpersForGroups.CreateGroup(new()),
                });

        private static Action<ExaminationTestData, Examination> RemoveGroup = (data, exam) => 
            exam.RemoveGroup(data.Groups[0]);

        private static Action<ExaminationTestData, Examination> RemoveGroups = (data, exam) =>
            exam.RemoveGroups(
                new List<Group>
                {
                    data.Groups[0],
                    data.Groups[1],
                });
    }
}
