using AlphaTest.Core.Answers;
using AlphaTest.Core.Checking;
using System;

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

        public override PreliminaryResult CheckAnswer(Answer answer)
        {
            // MAYBE стоит куда-то вынести, так как похоже на нарушение SRP
            if (answer is null)
                throw new ArgumentNullException(nameof(answer));
            if (answer is not ExactNumericAnswer convertedAnswer)
                throw new InvalidOperationException("Тип вопроса и тип ответа не соответствуют.");

            if (RightAnswer == convertedAnswer.Value)
                return new PreliminaryResult(Score.Value, CheckResultType.Credited, Score);
            else
                return new PreliminaryResult(0, CheckResultType.NotCredited, Score);
        }
    }
}
