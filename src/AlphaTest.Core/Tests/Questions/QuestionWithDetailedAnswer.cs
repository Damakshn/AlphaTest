namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithDetailedAnswer: Question
    {
        private QuestionWithDetailedAnswer():base() { }

        internal QuestionWithDetailedAnswer(int testID, string text, uint number, uint score)
            : base(testID, text, number, score) { }

    }
}
