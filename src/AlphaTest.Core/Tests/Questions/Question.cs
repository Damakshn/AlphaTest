﻿using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.Questions.Rules;
using System;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class Question: Entity
    {
        #region Свойства
        public Guid ID { get; protected set; }

        public Guid TestID { get; protected set; }

        public QuestionText Text { get; protected set; }

        public uint Number { get; protected set; }

        public QuestionScore Score { get; protected set; }
        #endregion

        #region Конструкторы
        protected Question(){}

        protected Question(Guid testID, QuestionText text, uint number, QuestionScore score)
        {
            CheckCommonRules(score);
            ID = Guid.NewGuid();
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
            // ToDo unit test this
            CheckRule(new NonDraftTestCannotBeEditedRule(test));
            if (score is not null)
            {
                score = test.CalculateActualQuestionScore(score);
            }
            CheckRule(new QuestionScoreMustBeSpecifiedRule(score));
            Score = score;
        }

        public abstract Question ReplicateForNewEdition(Test newEdition);

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
