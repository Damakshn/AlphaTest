using Xunit;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    
    public class QuestionWithTextualAnswerTests: QuestionTestsBase
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateQuestionWithTextualAnswer_With_NullOrWhiteSpaceAnswer_IsNotPossible(string rightAnswer)
        {
            QuestionTestData data = new() { TextualAnswer = rightAnswer };

            AssertBrokenRule<TextualRightAnswerCannotBeNullOrWhitespaceRule>(() =>
                CreateQuestionWithTextualAnswer(data)
            );
        }
    }
}
