using AlphaTest.Core.Answers;

namespace AlphaTest.Core.Tests.Questions
{
    public interface IAutoCheckQuestion
    {
        public bool IsRight(Answer answer);
    }
}
