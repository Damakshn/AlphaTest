using AlphaTest.Core.Tests.Questions.Rules;
using System;

namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithTextualAnswer: QuestionWithExactAnswer<string>
    {
        private QuestionWithTextualAnswer() { }

        internal QuestionWithTextualAnswer(Guid testID, QuestionText text, uint number, QuestionScore score, string rightAnswer) :
            base(testID, text, number, score, rightAnswer)
        {
            CheckRule(new TextualRightAnswerCannotBeNullOrWhitespaceRule(rightAnswer));
        }

        public override void ChangeRightAnswer(string newRightAnswer)
        {
            CheckRule(new TextualRightAnswerCannotBeNullOrWhitespaceRule(newRightAnswer));
            RightAnswer = newRightAnswer;
        }

        public override QuestionWithTextualAnswer ReplicateForNewEdition(Test newEdition)
        {
            QuestionWithTextualAnswer replica = (QuestionWithTextualAnswer)this.MemberwiseClone();
            replica.ID = Guid.NewGuid();
            replica.TestID = newEdition.ID;
            return replica;
        }
    }
}
