namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithNumericAnswer: QuestionWithExactAnswer<decimal>
    {
        private QuestionWithNumericAnswer() { }

        internal QuestionWithNumericAnswer(int testID, string text, uint number, QuestionScore score, decimal rightAnswer) :
            base(testID, text, number, score, rightAnswer) { }
    }
}
