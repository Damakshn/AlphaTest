using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.TestSettings.Checking;
using Xunit;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionWithNumericAnswerTests : QuestionWithExactAnswerTestsBase
    {
        [Fact]
        public void WhenCreateQuestionWithNumericAnswer_WithUnifiedScore_ScoreArgumentIsIgnored()
        {
            QuestionScore unifiedScore = new(10);
            QuestionScore userScore = new(5);
            QuestionWithExactAnswerTestData<decimal> data = new() { RightAnswer = 42 };
            data.Test.ChangeScoreDistributionMethod(ScoreDistributionMethod.UNIFIED);
            data.Test.ChangeScorePerQuestion(unifiedScore);

            QuestionWithNumericAnswer question = data.Test.AddQuestionWithNumericAnswer(data.Text, data.Score, data.RightAnswer, data.CounterMock.Object);

            Assert.Equal(unifiedScore, question.Score);
            Assert.NotEqual(userScore, question.Score);
        }
    }
}
