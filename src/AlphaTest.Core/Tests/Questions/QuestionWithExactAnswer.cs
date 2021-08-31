namespace AlphaTest.Core.Tests.Questions
{
    public abstract class QuestionWithExactAnswer<TDecimalOrString>: Question
    {
        public static string AnswerType => typeof(TDecimalOrString).ToString();

        public TDecimalOrString RightAnswer { get; private set; }

        protected QuestionWithExactAnswer() { }

        protected QuestionWithExactAnswer(int testID, QuestionText text, uint number, QuestionScore score, TDecimalOrString rightAnswer):
            base(testID, text, number, score)
        {
            RightAnswer = rightAnswer;
        }

        internal void ChangeAttributes(string text, QuestionScore score, TDecimalOrString rightAnswer)
        {   
            RightAnswer = rightAnswer;
            Score = score;
        }
    }
}
