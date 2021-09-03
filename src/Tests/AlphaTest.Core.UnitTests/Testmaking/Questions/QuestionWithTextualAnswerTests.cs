using Xunit;
using AlphaTest.Core.Tests.Questions.Rules;
using AlphaTest.Core.UnitTests.Common;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    
    public class QuestionWithTextualAnswerTests: UnitTestBase
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateQuestionWithTextualAnswer_With_NullOrWhiteSpaceAnswer_IsNotPossible(string rightAnswer)
        {
            QuestionTestData data = new() { TextualAnswer = rightAnswer };

            AssertBrokenRule<TextualRightAnswerCannotBeNullOrWhitespaceRule>(() =>
                QuestionTestSamples.CreateQuestionWithTextualAnswer(data)
            );
        }
    }
}
