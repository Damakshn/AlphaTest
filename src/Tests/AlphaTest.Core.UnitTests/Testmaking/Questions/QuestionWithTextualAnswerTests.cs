using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.TestSettings.Checking;
using Xunit;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionWithTextualAnswerTests: QuestionWithExactAnswerTestsBase
    {
        [Fact]
        public void WhenCreateQuestionWithTextualAnswer_WithUnifiedScore_ScoreArgumentIsIgnored()
        {
            QuestionScore unifiedScore = new(10);
            QuestionScore userScore = new(5);
            QuestionWithExactAnswerTestData<string> data = new() { RightAnswer = "Правильный ответ" };
            data.Test.ChangeScoreDistributionMethod(ScoreDistributionMethod.UNIFIED);
            data.Test.ChangeScorePerQuestion(unifiedScore);

            QuestionWithTextualAnswer question = data.Test.AddQuestionWithTextualAnswer(data.Text, data.Score, data.RightAnswer, data.CounterMock.Object);

            Assert.Equal(unifiedScore, question.Score);
            Assert.NotEqual(userScore, question.Score);
        }
    }
}
