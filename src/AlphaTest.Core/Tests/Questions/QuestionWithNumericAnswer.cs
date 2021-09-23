﻿using System;

namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithNumericAnswer: QuestionWithExactAnswer<decimal>
    {
        private QuestionWithNumericAnswer() { }

        internal QuestionWithNumericAnswer(Guid testID, QuestionText text, uint number, QuestionScore score, decimal rightAnswer) :
            base(testID, text, number, score, rightAnswer) { }

        public override void ChangeRightAnswer(decimal newRightAnswer)
        {
            RightAnswer = newRightAnswer;
        }

        public override QuestionWithNumericAnswer ReplicateForNewEdition(Test newEdition)
        {
            QuestionWithNumericAnswer replica = (QuestionWithNumericAnswer)this.MemberwiseClone();
            replica.ID = Guid.NewGuid();
            replica.TestID = newEdition.ID;
            replica.ID = default;
            return replica;
        }
    }
}
