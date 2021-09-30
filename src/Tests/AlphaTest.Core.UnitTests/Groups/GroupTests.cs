using System;
using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.Groups.Rules;
using AlphaTest.Core.Groups;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Users;
using System.Linq;
using System.Collections.Generic;
using AlphaTest.Core.UnitTests.Fixtures;

namespace AlphaTest.Core.UnitTests.Groups
{
    public class GroupTests: UnitTestBase
    {   
        [Fact]
        public void Group_must_be_unique()
        {
            GroupTestData data = new() { GroupAlreadyExists = true };

            AssertBrokenRule<GroupMustBeUniqueRule>(() =>
                new Group(data.Name, data.BeginDate, data.EndDate, data.GroupAlreadyExists)
            );
        }

        [Fact]
        public void Group_cannot_be_created_in_the_past()
        {
            GroupTestData data = new()
            {
                BeginDate = DateTime.Now.AddDays(-10),
                EndDate = DateTime.Now.AddDays(50)
            };
            AssertBrokenRule<GroupCannotBeCreatedInThePastRule>(() =>
                new Group(data.Name, data.BeginDate, data.EndDate, data.GroupAlreadyExists)
            );
        }

        [Fact]
        public void Group_end_date_must_follow_begin_date()
        {
            GroupTestData data = new()
            {
                BeginDate = DateTime.Now.AddDays(50),
                EndDate = DateTime.Now.AddDays(10)
            };
            AssertBrokenRule<GroupEndDateMustFollowBeginDateRule>(() =>
                new Group(data.Name, data.BeginDate, data.EndDate, data.GroupAlreadyExists)
            );
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Group_name_must_be_provided(string groupName)
        {
            GroupTestData data = new()
            {
                Name = groupName
            };
            AssertBrokenRule<GroupNameMustBeProvidedRule>(() =>
                new Group(data.Name, data.BeginDate, data.EndDate, data.GroupAlreadyExists)
            );
        }

        [Theory, GroupTestsData]
        public void Group_maximum_size_is_limited(Group group)
        {
            for (int i = 0; i < 100; i++)
            {
                UserTestData userData = new() { InitialRole = UserRole.STUDENT };
                group.AddStudent(HelpersForUsers.CreateUser(userData));
            }

            UserTestData moreUserData = new() { InitialRole = UserRole.STUDENT };
            User extraStudent = HelpersForUsers.CreateUser(moreUserData);
            AssertBrokenRule<GroupSizeIsLimitedRule>(() =>
                group.AddStudent(extraStudent)
            );
        }

        [Theory]
        [GroupTestsMemberData(nameof(NonStudentRoles))]
        public void Only_students_can_be_members_of_group(int roleID, Group group)
        {
            UserTestData userData = new() { InitialRole = UserRole.ParseFromID(roleID) };
            User candidate = HelpersForUsers.CreateUser(userData);

            AssertBrokenRule<OnlyStudentsCanBeIncludedIntoGroupRule>(() =>
                group.AddStudent(candidate)
            );
        }

        [Theory, GroupTestsData]
        public void Students_can_be_added_to_group(Group group)
        {
            UserTestData userData = new() {InitialRole = UserRole.STUDENT };
            User candidate = HelpersForUsers.CreateUser(userData);
            group.AddStudent(candidate);

            Assert.Equal(1, group.Memberships.Count(m => m.StudentID == candidate.ID));
        }

        [Theory, GroupTestsData]
        public void Students_cannot_be_added_to_disbanded_group(Group group)
        {
            UserTestData userData = new() { InitialRole = UserRole.STUDENT };
            User candidate = HelpersForUsers.CreateUser(userData);
            group.Disband();

            AssertBrokenRule<DisbandedGroupCannotBeModifiedRule>(() =>
                group.AddStudent(candidate)
            );
        }

        [Theory, GroupTestsData]
        public void Students_cannot_be_added_to_outdated_group(Group group)
        {
            UserTestData userData = new() { InitialRole = UserRole.STUDENT };
            User candidate = HelpersForUsers.CreateUser(userData);
            HelpersForGroups.SetGroupDates(group, DateTime.Now.AddDays(-365), DateTime.Now.AddDays(-100));
            
            AssertBrokenRule<InactiveGroupCannotBeModifiedRule>(() => group.AddStudent(candidate));
        }

        [Theory, GroupTestsData]
        public void Group_can_be_disbanded(Group group)
        {
            group.Disband();

            Assert.True(group.IsDisbanded);
        }

        [Theory, GroupTestsData]
        public void Disbanded_group_can_be_restored(Group group)
        {
            group.Disband();

            Assert.True(group.IsDisbanded);
            group.Restore();
            Assert.False(group.IsDisbanded);
        }

        [Theory, GroupTestsData]
        public void Group_can_be_disbanded_only_once(Group group)
        {
            group.Disband();

            AssertBrokenRule<DisbandedGroupCannotBeModifiedRule>(() =>
                group.Disband()
            );
        }

        [Theory, GroupTestsData]
        public void Outdated_group_cannot_be_disbanded(Group group)
        {
            HelpersForGroups.SetGroupDates(group, DateTime.Now.AddDays(-365), DateTime.Now.AddDays(-100));

            AssertBrokenRule<InactiveGroupCannotBeModifiedRule>(() =>
                group.Disband()
            );
        }

        [Theory, GroupTestsData]
        public void Outdated_disbanded_group_can_be_restored(Group group)
        {
            group.Disband();
            HelpersForGroups.SetGroupDates(group, DateTime.Now.AddDays(-365), DateTime.Now.AddDays(-100));
            Assert.True(group.IsDisbanded);
            Assert.True(group.IsGone);

            group.Restore();

            Assert.True(group.IsGone);
            Assert.False(group.IsDisbanded);
        }

        [Theory, GroupTestsData]
        public void Non_member_student_cannot_be_excluded_from_group(Group group)
        {
            User userInGroup = HelpersForUsers.CreateUser(new() { InitialRole = UserRole.STUDENT });
            group.AddStudent(userInGroup);

            User userNotInGroup = HelpersForUsers.CreateUser(new() { InitialRole = UserRole.STUDENT });
            AssertBrokenRule<NonMemberStudentsCannotBeExcludedFromGroupRule>(() =>
                group.ExcludeStudent(userNotInGroup)
            );
            
        }

        [Theory, GroupTestsData]
        public void Student_can_be_excluded_from_group(Group group)
        {
            User student = HelpersForUsers.CreateUser(new() { InitialRole = UserRole.STUDENT });
            group.AddStudent(student);

            Assert.Equal(1, group.Memberships.Count(m => m.StudentID == student.ID));
            group.ExcludeStudent(student);
            Assert.Equal(0, group.Memberships.Count(m => m.StudentID == student.ID));
        }

        [Theory, GroupTestsData]
        public void Student_cannot_be_excluded_from_disbanded_group(Group group)
        {
            User student = HelpersForUsers.CreateUser(new() { InitialRole = UserRole.STUDENT });
            group.AddStudent(student);
            Assert.Equal(1, group.Memberships.Count(m => m.StudentID == student.ID));
            group.Disband();

            AssertBrokenRule<DisbandedGroupCannotBeModifiedRule>(() => group.ExcludeStudent(student));
        }

        [Theory, GroupTestsData]
        public void Student_cannot_be_excluded_from_outdated_group(Group group)
        {
            User student = HelpersForUsers.CreateUser(new() { InitialRole = UserRole.STUDENT });
            group.AddStudent(student);
            Assert.Equal(1, group.Memberships.Count(m => m.StudentID == student.ID));
            HelpersForGroups.SetGroupDates(group, DateTime.Now.AddDays(-365), DateTime.Now.AddDays(-100));

            AssertBrokenRule<InactiveGroupCannotBeModifiedRule>(() => group.ExcludeStudent(student));
        }

        [Theory, GroupTestsData]
        public void Group_begin_date_cannot_be_moved_to_the_past(Group group)
        {
            AssertBrokenRule<GroupCannotBeCreatedInThePastRule>(() =>
                group.ChangeDates(DateTime.Now.AddDays(-100), group.EndDate)
            );
        }

        [Theory, GroupTestsData]
        public void New_end_date_must_follow_begin_date(Group group)
        {
            var newBeginDate = DateTime.Now.AddDays(100);
            var newEndDate = DateTime.Now.AddDays(10);

            AssertBrokenRule<GroupEndDateMustFollowBeginDateRule>(() =>
                group.ChangeDates(newBeginDate, newEndDate)
            );
        }

        [Theory, GroupTestsData]
        public void Dates_of_disbanded_group_cannot_be_changed(Group group)
        {
            var newBeginDate = DateTime.Now.AddDays(10);
            var newEndDate = DateTime.Now.AddDays(100);
            group.Disband();
            AssertBrokenRule<DisbandedGroupCannotBeModifiedRule>(() =>
                group.ChangeDates(newBeginDate, newEndDate)
            );
        }

        [Theory, GroupTestsData]
        public void Group_dates_can_be_changed(Group group)
        {
            var newBeginDate = DateTime.Now.AddDays(10);
            var newEndDate = DateTime.Now.AddDays(100);
            group.ChangeDates(newBeginDate, newEndDate);

            Assert.Equal(newBeginDate, group.BeginDate);
            Assert.Equal(newEndDate, group.EndDate);
        }

        [Theory, GroupTestsData]
        public void Group_must_remain_unique_after_renaming(Group group)
        {
            AssertBrokenRule<GroupMustBeUniqueRule>(() =>
                group.Rename("ТакаяГруппаУжеЕсть",true)
            );
        }

        [Theory]
        [GroupTestsInlineData(null)]
        [GroupTestsInlineData("")]
        [GroupTestsInlineData("   ")]
        public void New_name_for_group_cannot_be_empty(string newName, Group group)
        {
            AssertBrokenRule<GroupNameMustBeProvidedRule>(() =>
                group.Rename(newName, false)
            );
        }

        [Theory, GroupTestsData]
        public void Disbanded_group_cannot_be_renamed(Group group)
        {
            group.Disband();
            AssertBrokenRule<DisbandedGroupCannotBeModifiedRule>(() =>
                group.Rename("Новое название", false)
            );
        }

        [Theory, GroupTestsData]
        public void Group_can_be_renamed(Group group)
        {
            string newName = "НовоеНазвание12345";
            group.Rename(newName, false);

            Assert.Equal(newName, group.Name);
        }

        [Theory, GroupTestsData]
        public void Outdated_group_cannot_be_renamed(Group group)
        {
            HelpersForGroups.SetGroupDates(group, DateTime.Now.AddDays(-365), DateTime.Now.AddDays(-100));

            AssertBrokenRule<InactiveGroupCannotBeModifiedRule>(() => group.Rename("НовоеНазвание", false));
        }

        public static IEnumerable<object[]> NonStudentRoles => UserRole.All
            .Where(r => r != UserRole.STUDENT)
            .Select(r => new object[] { r.ID })
            .ToList();


    }
}
