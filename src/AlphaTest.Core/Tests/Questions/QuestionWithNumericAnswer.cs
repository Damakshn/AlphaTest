using AlphaTest.Core.Answers;
using System;

namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithNumericAnswer: QuestionWithExactAnswer<decimal>
    {
        private QuestionWithNumericAnswer() { }

        internal QuestionWithNumericAnswer(int testID, QuestionText text, uint number, QuestionScore score, decimal rightAnswer) :
            base(testID, text, number, score, rightAnswer) { }

        public override void ChangeRightAnswer(decimal newRightAnswer)
        {
            RightAnswer = newRightAnswer;
        }

        public override QuestionWithNumericAnswer ReplicateForNewEdition(Test newEdition)
        {
            QuestionWithNumericAnswer replica = (QuestionWithNumericAnswer)this.MemberwiseClone();
            replica.TestID = newEdition.ID;
            replica.ID = default;
            return replica;
        }

        public override bool IsRight(Answer answer)
        {
            if (answer is null)
                throw new ArgumentNullException(nameof(answer));
            if (answer is not ExactNumericAnswer convertedAnswer)
                throw new InvalidOperationException("Тип вопроса и тип ответа не соответствуют.");
            return RightAnswer == convertedAnswer.Value;
        }
    }
}
