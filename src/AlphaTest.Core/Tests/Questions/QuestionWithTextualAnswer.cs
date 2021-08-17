namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithTextualAnswer: QuestionWithExactAnswer<string>
    {
        private QuestionWithTextualAnswer() { }

        internal QuestionWithTextualAnswer(string text, uint number, uint score, string rightAnswer) :
            base(text, number, score, rightAnswer){ }

    }
}
