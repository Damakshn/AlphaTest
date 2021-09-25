using AlphaTest.Core.Answers;
using AlphaTest.Core.Checking;
using System;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class QuestionWithExactAnswer<TDecimalOrString> : Question, IAutoCheckQuestion
    {
        public static string AnswerType => typeof(TDecimalOrString).ToString();

        public TDecimalOrString RightAnswer { get; protected set; }

        protected QuestionWithExactAnswer() { }

        protected QuestionWithExactAnswer(Guid testID, QuestionText text, uint number, QuestionScore score, TDecimalOrString rightAnswer):
            base(testID, text, number, score)
        {
            RightAnswer = rightAnswer;
        }

        public abstract void ChangeRightAnswer(TDecimalOrString newRightAnswer);
        public abstract bool IsRight(Answer answer);
        public abstract PreliminaryResult CheckAnswer(Answer answer);
    }
}
