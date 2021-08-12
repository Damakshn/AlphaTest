namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithExactAnswer<T>: Question
    {
        public T RightAnswer { get; private set; }
    }
}
