using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Questions.Rules;
using System;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class Question: Entity
    {
        #region Свойства
        public int ID { get; protected set; }

        public int TestID { get; protected set; }

        public QuestionText Text { get; protected set; }

        public uint Number { get; protected set; }

        public QuestionScore Score { get; protected set; }
        #endregion

        #region Конструкторы
        protected Question(){}

        protected Question(int testID, QuestionText text, uint number, QuestionScore score)
        {
            CheckCommonRules(score);
            TestID = testID;
            Text = text;
            Number = number;
            Score = score;
        }
        #endregion

        #region Методы
        public void ChangeScore(Test test, QuestionScore score)
        {
            if (test is null) throw new ArgumentNullException(nameof(test));
            if (score is not null)
            {
                score = test.CalculateActualQuestionScore(score);
            }
            CheckRule(new QuestionScoreMustBeSpecifiedRule(score));
            Score = score;
        }

        internal void ChangeNumber(uint number)
        {
            Number = number;
        }

        private void CheckCommonRules(QuestionScore score)
        {
            CheckRule(new QuestionScoreMustBeSpecifiedRule(score));
        }
        #endregion

    }
}
