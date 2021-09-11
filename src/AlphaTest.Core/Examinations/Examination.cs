﻿using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Examinations.Rules;
using AlphaTest.Core.Users;
using System.Collections.Generic;
using AlphaTest.Core.Groups;
using System.Linq;

namespace AlphaTest.Core.Examinations
{
    public class Examination : Entity
    {
        #region Поля
        private List<ExamParticipation> _participations;
        #endregion

        #region Конструкторы
        private Examination() { }
        
        public Examination(Test test, DateTime startsAt, DateTime endsAt, User examiner, IEnumerable<Group> groups)
        {
            /*
            ToDo
	        Для одной или нескольких групп, которым пользователь пытается назначить экзамен, уже назначен экзамен по данному тесту и периоды сдачи этих экзаменов перекрываются.
	            Система выводит пользователю сообщение об ошибке с указанием перекрывающихся периодов.
            Передача ID в конструктор
            */
            CheckRule(new ExaminationsCanBeCreatedOnlyForPublishedTestsRule(test));
            CheckCommonRulesForDatesAndDuration(startsAt, endsAt, test);
            CheckCommonRulesForExaminer(examiner, test);
            CheckRule(new DisbandedOrInactiveGroupsCannotParticipateExamRule(groups));
            TestID = test.ID;
            ExaminerID = examiner.ID;
            StartsAt = startsAt;
            EndsAt = endsAt;
            IsCanceled = false;
            _participations = new List<ExamParticipation>();
            foreach(var group in groups)
            {
                _participations.Add(new ExamParticipation(this, group));
            }
        }
        #endregion

        #region Свойства
        public int ID { get; private set; }

        public int TestID { get; private set; }

        public int ExaminerID { get; private set; }

        public DateTime StartsAt {get; private set; }

        public DateTime EndsAt { get; private set; }

        public bool IsCanceled { get; private set; }

        public IReadOnlyCollection<ExamParticipation> Participations => _participations.AsReadOnly();
        #endregion

        #region Методы

        #region Редактирование
        public void ChangeDates(DateTime newStart, DateTime newEnd, Test test)
        {
            /*
             * ToDo
             * У одной или нескольких групп, которым назначен экзамен, 
             * есть другой экзамен по данному тесту и период сдачи этого 
             * экзамена перекрываются с новым сроком сдачи.
            */
            CheckCommonRulesForModification();
            CheckCommonRulesForDatesAndDuration(newStart, newEnd, test);
            CheckRule(new StartOfExamCannotBeMovedIfExamAlreadyStartedRule(this));
            StartsAt = newStart;
            EndsAt = newEnd;
            // ToDo domain event
        }

        public void Cancel()
        {
            CheckCommonRulesForModification();
            IsCanceled = true;
            // ToDo domain event
        }

        public void SwitchExaminer(User newExaminer, Test test)
        {
            CheckCommonRulesForModification();
            CheckCommonRulesForExaminer(newExaminer, test);
            ExaminerID = newExaminer.ID;
            // ToDo domain event
        }

        public void AddGroup(Group group)
        {
            CheckCommonRulesForModification();
            CheckRule(new DisbandedOrInactiveGroupsCannotParticipateExamRule(group));
            CheckRule(new GroupCanBeAddedToExamOnlyOnceRule(group, this));
            _participations.Add(new ExamParticipation(this, group));
            // todo domain event
        }

        public void AddGroups(IEnumerable<Group> groups)
        {
            CheckCommonRulesForModification();
            CheckRule(new DisbandedOrInactiveGroupsCannotParticipateExamRule(groups));
            CheckRule(new GroupCanBeAddedToExamOnlyOnceRule(groups, this));
            foreach (var group in groups)
            {
                _participations.Add(new ExamParticipation(this, group));
            }
            // todo domain event
        }

        public void RemoveGroup(Group group)
        {
            CheckCommonRulesForModification();
            CheckRule(new NonParticipatingGroupsCannotBeExcludedFromExamRule(group, this));
            var participationToRemove = _participations.Where(p => p.GroupID == group.ID).FirstOrDefault();
            _participations.Remove(participationToRemove);
            // todo domain event
        }

        public void RemoveGroups(IEnumerable<Group> groups)
        {
            CheckCommonRulesForModification();
            CheckRule(new NonParticipatingGroupsCannotBeExcludedFromExamRule(groups, this));
            foreach(var group in groups)
            {
                var participationToRemove = _participations.Where(p => p.GroupID == group.ID).FirstOrDefault();
                _participations.Remove(participationToRemove);
            }
            // todo domain event
        }
        #endregion

        #region Сгруппированные проверки правил
        private void CheckCommonRulesForExaminer(User examiner, Test test)
        {
            CheckRule(new ExaminerMustBeTeacherRule(examiner));
            CheckRule(new ExaminerMustBeAuthorOrContributorOfTheTestRule(examiner, test));
        }

        private void CheckCommonRulesForModification()
        {
            CheckRule(new CanceledExaminationCannotBeModifiedRule(this));
            CheckRule(new EndedExamCannotBeModifiedRule(this));
        }

        private void CheckCommonRulesForDatesAndDuration(DateTime start, DateTime end, Test test)
        {
            CheckRule(new StartOfExaminationMustBeEarlierThanEndRule(start, end));
            CheckRule(new ExaminationCannotBeCreatedThePastRule(start, end));
            CheckRule(new ExamDurationCannotBeShorterThanTimeLimitInTestRule(start, end, test));
        }
        #endregion

        #endregion
    }
}
