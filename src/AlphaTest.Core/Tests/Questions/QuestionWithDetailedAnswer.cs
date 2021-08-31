namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithDetailedAnswer: Question
    {
        private QuestionWithDetailedAnswer():base() { }

        internal QuestionWithDetailedAnswer(int testID, QuestionText text, uint number, QuestionScore score)
            : base(testID, text, number, score) { }

    }
}
