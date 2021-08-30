using System;
using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Rules;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionScoreTests: UnitTestBase
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(101)]
        public void SetQuestionScore_OutOfRange_IsNotPossible(int value)
        {
            Action act = () => new QuestionScore(value);
            AssertBrokenRule<QuestionScoreMustBeInRangeRule>(act);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(85)]
        [InlineData(100)]
        public void SetQuestionScore_WithinRange_IsOk(int value)
        {
            QuestionScore score = new(value);
            Assert.Equal(value, score.Value);
        }
    }
}
