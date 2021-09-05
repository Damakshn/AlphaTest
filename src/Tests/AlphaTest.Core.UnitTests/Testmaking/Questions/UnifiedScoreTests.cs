using System;
using Xunit;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests;
using AlphaTest.Core.UnitTests.Common;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class UnifiedScoreTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceAllTypesOfQuestions), MemberType = typeof(QuestionTestSamples))]
        public void CreateQuestion_WithCustomScore_WhenScoreIsUnified_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionScore unifiedScore = new(10);
            QuestionScore customScore = new(5);
            QuestionTestData data = new() { Score = customScore };
            data.Test.ConfigureScoreDistribution(ScoreDistributionMethod.UNIFIED, unifiedScore);

            AssertBrokenRule<UnifiedScoreCannotBeReplacedWithDifferentScoreRule>(() =>
                createQuestionDelegate(data)
            );
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceAllTypesOfQuestions), MemberType = typeof(QuestionTestSamples))]
        public void CreateQuestion_WhenScoreIsUnified_Expect_AnyQuestionHasUnifiedScore(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionScore unifiedScore = new(10);
            QuestionTestData data = new() { Score = null };
            data.Test.ConfigureScoreDistribution(ScoreDistributionMethod.UNIFIED, unifiedScore);

            Question question = createQuestionDelegate(data);

            Assert.Equal(unifiedScore, question.Score);
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceAllTypesOfQuestions), MemberType = typeof(QuestionTestSamples))]
        public void ReplaceQuestionScore_FromNonUnified_ToUnified_IsOk (Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionScore unifiedScore = new(10);
            QuestionScore customScore = new(5);
            QuestionTestData data = new() { Score = customScore };
            Question question = createQuestionDelegate(data);

            Assert.Equal(customScore, question.Score);
            data.Test.ConfigureScoreDistribution(ScoreDistributionMethod.UNIFIED, unifiedScore);
            question.ChangeScore(data.Test, unifiedScore);

            Assert.Equal(unifiedScore, question.Score);
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceAllTypesOfQuestions), MemberType = typeof(QuestionTestSamples))]
        public void ReplaceQuestionScore_FromUnified_ToNonUnified_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionScore unifiedScore = new(10);
            QuestionScore customScore = new(5);
            QuestionTestData data = new() { Score = unifiedScore };
            data.Test.ConfigureScoreDistribution(ScoreDistributionMethod.UNIFIED, unifiedScore);
            Question question = createQuestionDelegate(data);

            Assert.Equal(unifiedScore, question.Score);
            AssertBrokenRule<UnifiedScoreCannotBeReplacedWithDifferentScoreRule>(() =>
                question.ChangeScore(data.Test, customScore)
            );
        }
    }
}
