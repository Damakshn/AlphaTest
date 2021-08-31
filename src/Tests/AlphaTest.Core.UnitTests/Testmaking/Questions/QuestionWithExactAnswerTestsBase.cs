namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionWithExactAnswerTestsBase: QuestionTestsBase
    {
        protected class QuestionWithExactAnswerTestData<TDecimalOrString> : QuestionTestData
        {
            internal TDecimalOrString RightAnswer { get; set; }
        }
    }
}
