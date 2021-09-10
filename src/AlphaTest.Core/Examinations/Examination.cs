using System;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Examinations.Rules;
using AlphaTest.Core.Users;

namespace AlphaTest.Core.Examinations
{
    public class Examination : Entity
    {
        private Examination() { }

        // ToDo список групп, участвующих в экзамене
        public Examination(Test test, DateTime startsAt, DateTime endsAt, User examiner)
        {
            CheckRule(new ExaminationsCanBeCreatedOnlyForPublishedTestsRule(test));
            CheckRule(new ExaminationCannotStartInThePastRule(startsAt));
            CheckRule(new StartOfExaminationMustBeEarlierThanEndRule(startsAt, endsAt));
            CheckRule(new ExamDurationCannotBeShorterThanTimeLimitInTestRule(startsAt, endsAt, test));
            CheckRulesForExaminer(examiner, test);
            /*
            ToDo
	        Одна или несколько групп, для которых был назначен экзамен, были расформированы.
	        Срок существования одной или нескольких групп, которым был назначен экзамен, истёк.	        
	        Для одной или нескольких групп, которым пользователь пытается назначить экзамен, уже назначен экзамен по данному тесту и периоды сдачи этих экзаменов перекрываются.
	            Система выводит пользователю сообщение об ошибке с указанием перекрывающихся периодов.
            */

            TestID = test.ID;
            ExaminerID = examiner.ID;
            StartsAt = startsAt;
            EndsAt = endsAt;
            Canceled = false;
        }

        public int ID { get; private set; }

        public int TestID { get; private set; }

        public int ExaminerID { get; private set; }

        public DateTime StartsAt {get; private set; }

        public DateTime EndsAt { get; private set; }

        public bool Canceled { get; private set; }

        public void ChangeDates(DateTime? newStart = null, DateTime? newEnd = null)
        {
            throw new System.NotImplementedException();
        }

        public void Cancel()
        {
            throw new System.NotImplementedException();
        }

        public void SwitchExaminer(User newExaminer, Test test)
        {
            CheckRulesForExaminer(newExaminer, test);
            ExaminerID = newExaminer.ID;
        }

        private void CheckRulesForExaminer(User examiner, Test test)
        {
            CheckRule(new ExaminerMustBeTeacherRule(examiner));
            CheckRule(new ExaminerMustBeAuthorOrContributorOfTheTestRule(examiner, test));
        }
    }
}
