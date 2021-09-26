using AlphaTest.Core.Checking;

namespace AlphaTest.Core.Tests.Questions
{
    public interface IAutoCheckQuestion
    {
        public PreliminaryResult AcceptCheckingVisitor(CheckingVisitor visitor);
    }
}
