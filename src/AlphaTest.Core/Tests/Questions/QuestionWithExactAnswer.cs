namespace AlphaTest.Core.Tests.Questions
{
    public abstract class QuestionWithExactAnswer<TDecimalOrString>: Question
    {
        public static string AnswerType => typeof(TDecimalOrString).ToString();

        public TDecimalOrString RightAnswer { get; private set; }

        protected QuestionWithExactAnswer() { }

        protected QuestionWithExactAnswer(int testID, string text, uint number, uint score, TDecimalOrString rightAnswer):
            base(testID, text, number, score)
        {
            RightAnswer = rightAnswer;
        }

        internal void ChangeAttributes(string text, uint score, TDecimalOrString rightAnswer)
        {
            CheckRulesForTextAndScore(text, score);
            RightAnswer = rightAnswer;
        }
    }
}
