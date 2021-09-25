using AlphaTest.Core.Answers;
using AlphaTest.Core.Checking;

namespace AlphaTest.Core.Tests.Questions
{
    public interface IAutoCheckQuestion
    {
        public bool IsRight(Answer answer);

        public PreliminaryResult CheckAnswer(Answer answer);
    }
}
