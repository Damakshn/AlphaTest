using System.Collections.Generic;
using Xunit;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.TestSettings.Checking;
using System.Linq;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionWithDetailedAnswerTests: QuestionTestsBase
    {   
        [Fact]
        public void CreateQuestionWithDetailedAnswer_WhenCheckingMethodIsAutomatic_IsNotPossible()
        {
            QuestionTestData data = new();
            data.Test.ChangeWorkCheckingMethod(WorkCheckingMethod.AUTOMATIC, new List<Question>());
         
            AssertBrokenRule<QuestionsWithDetailedAnswersNotAllowedWithAutomatedCheckRule>(() => 
                data.Test.AddQuestionWithDetailedAnswer(data.Text, data.Score, data.CounterMock.Object)
            );
        }

        [Theory]
        [MemberData(nameof(NonAutomaticCheckingMethods))]
        public void CreateQuestionWithDetailedAnswer_WhenCheckingMethodNotAutomatic_IsOk(WorkCheckingMethod checkingMethod)
        {
            QuestionTestData data = new();
            data.Test.ChangeWorkCheckingMethod(checkingMethod, new List<Question>());
            
            QuestionWithDetailedAnswer question = data.Test.AddQuestionWithDetailedAnswer(data.Text, data.Score, data.CounterMock.Object);
            
            Assert.Equal(data.Test.ID, question.TestID);
            Assert.Equal(data.Score, question.Score);
        }

        [Fact]
        public void WhenCreateQuestionWithDetailedAnswer_WithUnifiedScore_ScoreArgumentIsIgnored()
        {
            QuestionScore unifiedScore = new(10);
            QuestionScore userScore = new(5);
            QuestionTestData data = new();
            data.Test.ChangeScoreDistributionMethod(ScoreDistributionMethod.UNIFIED);
            data.Test.ChangeScorePerQuestion(unifiedScore);

            QuestionWithDetailedAnswer question = data.Test.AddQuestionWithDetailedAnswer(data.Text, data.Score, data.CounterMock.Object);

            Assert.Equal(unifiedScore, question.Score);
            Assert.NotEqual(userScore, question.Score);
        }

        public static IEnumerable<object[]> NonAutomaticCheckingMethods =>
            WorkCheckingMethod.All
            .Where(m => m != WorkCheckingMethod.AUTOMATIC)
            .Select(m => new object[] { m })
            .ToList();
    }
}
