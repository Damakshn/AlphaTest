namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithTextualAnswer: QuestionWithExactAnswer<string>
    {
        private QuestionWithTextualAnswer() { }

        internal QuestionWithTextualAnswer(int testID, string text, uint number, uint score, string rightAnswer) :
            base(testID, text, number, score, rightAnswer){ }

    }
}
