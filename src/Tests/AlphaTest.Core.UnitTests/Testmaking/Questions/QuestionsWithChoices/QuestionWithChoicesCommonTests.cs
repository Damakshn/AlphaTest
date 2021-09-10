using AlphaTest.Core.Tests.Questions.Rules;
using System;
using System.Collections.Generic;
using AlphaTest.Core.Tests.Questions;
using Xunit;
using AlphaTest.Core.UnitTests.Common;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions.QuestionsWithChoices
{
    public class QuestionWithChoicesCommonTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceQuestionsWithChoices), MemberType = typeof(QuestionTestSamples))]
        public void CreateAnyQuestionWithChoices_WithoutOptions_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new() { Options = null };

            AssertBrokenRule<QuestionOptionsMustBeSpecifiedRule>(() =>
                createQuestionDelegate(data)
            );
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceQuestionsWithChoices), MemberType = typeof(QuestionTestSamples))]
        public void CreateAnyQuestionWithChoices_WithTooManyOptions_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new() { Options = QuestionWithChoicesTestSamples.QuestionOptionsTooMany };

            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(() =>
                createQuestionDelegate(data)
            );
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceQuestionsWithChoices), MemberType = typeof(QuestionTestSamples))]
        public void CreateAnyQuestionWithChoices_WithTooFewOptions_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new() { Options = QuestionWithChoicesTestSamples.QuestionOptionsTooFew };

            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(() =>
                createQuestionDelegate(data)
            );
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceQuestionsWithChoices), MemberType = typeof(QuestionTestSamples))]
        public void CreateAnyQuestionWithChoices_When_NumberOfOptions_WithinRange_IsOk(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new();

            QuestionWithChoices question = createQuestionDelegate(data) as QuestionWithChoices;

            Assert.Equal(data.Test.ID, question.TestID);
            Assert.Equal(data.Score, question.Score);
            Assert.Equal(data.Options.Count, question.Options.Count);
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceQuestionsWithChoices), MemberType = typeof(QuestionTestSamples))]
        public void ChangeOptions_ToNull_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new();
            QuestionWithChoices question = createQuestionDelegate(data) as QuestionWithChoices;

            AssertBrokenRule<QuestionOptionsMustBeSpecifiedRule>(() =>
                question.ChangeOptions(null)
            );
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceQuestionsWithChoices), MemberType = typeof(QuestionTestSamples))]
        public void ChangeOptions_ForTooMany_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new();
            
            QuestionWithChoices question = createQuestionDelegate(data) as QuestionWithChoices;

            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(() => question.ChangeOptions(QuestionWithChoicesTestSamples.QuestionOptionsTooMany));
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceQuestionsWithChoices), MemberType = typeof(QuestionTestSamples))]
        public void ChangeOptions_ForTooFew_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new();

            QuestionWithChoices question = createQuestionDelegate(data) as QuestionWithChoices;

            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(() => question.ChangeOptions(QuestionWithChoicesTestSamples.QuestionOptionsTooFew));
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceQuestionsWithChoices), MemberType = typeof(QuestionTestSamples))]
        public void ChangeOptions_When_NumberOfOptions_WithinRange_IsOk(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new();

            QuestionWithChoices question = createQuestionDelegate(data) as QuestionWithChoices;

            List<QuestionOption> newOptions = QuestionWithChoicesTestSamples.QuestionOptionsMinimum;
            question.ChangeOptions(QuestionWithChoicesTestSamples.QuestionOptionsMinimum);
            Assert.Equal(newOptions.Count, question.Options.Count);
        }
       
    }
}
