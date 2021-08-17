namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithNumericAnswer: QuestionWithExactAnswer<decimal>
    {
        private QuestionWithNumericAnswer() { }

        internal QuestionWithNumericAnswer(string text, uint number, uint score, decimal rightAnswer) :
            base(text, number, score, rightAnswer) { }
    }
}
