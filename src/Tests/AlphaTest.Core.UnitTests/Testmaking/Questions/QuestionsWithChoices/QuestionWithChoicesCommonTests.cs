using AlphaTest.Core.Tests.Questions.Rules;
using System;
using System.Collections.Generic;
using AlphaTest.Core.Tests.Questions;
using Xunit;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions.QuestionsWithChoices
{
    public class QuestionWithChoicesCommonTests: QuestionTestsBase
    {
        [Theory]
        [MemberData(nameof(InstanceQuestionsWithChoices))]
        public void CreateAnyQuestionWithChoices_WithoutOptions_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new() { Options = null };

            AssertBrokenRule<QuestionOptionsMustBeSpecifiedRule>(() =>
                createQuestionDelegate(data)
            );
        }

        [Theory]
        [MemberData(nameof(InstanceQuestionsWithChoices))]
        public void CreateAnyQuestionWithChoices_WithTooManyOptions_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new() { Options = QuestionOptionsTooMany };

            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(() =>
                createQuestionDelegate(data)
            );
        }

        [Theory]
        [MemberData(nameof(InstanceQuestionsWithChoices))]
        public void CreateAnyQuestionWithChoices_WithTooFewOptions_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new() { Options = QuestionOptionsTooFew };

            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(() =>
                createQuestionDelegate(data)
            );
        }

        [Theory]
        [MemberData(nameof(InstanceQuestionsWithChoices))]
        public void CreateAnyQuestionWithChoices_When_NumberOfOptions_WithinRange_IsOk(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new();

            QuestionWithChoices question = createQuestionDelegate(data) as QuestionWithChoices;

            Assert.Equal(data.Test.ID, question.TestID);
            Assert.Equal(data.Score, question.Score);
            Assert.Equal(data.Options.Count, question.Options.Count);
        }

        [Theory]
        [MemberData(nameof(InstanceQuestionsWithChoices))]
        public void ChangeOptions_ToNull_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new();
            QuestionWithChoices question = createQuestionDelegate(data) as QuestionWithChoices;

            AssertBrokenRule<QuestionOptionsMustBeSpecifiedRule>(() =>
                question.ChangeOptions(null)
            );
        }

        [Theory]
        [MemberData(nameof(InstanceQuestionsWithChoices))]
        public void ChangeOptions_ForTooMany_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new();
            
            QuestionWithChoices question = createQuestionDelegate(data) as QuestionWithChoices;

            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(() => question.ChangeOptions(QuestionOptionsTooMany));
        }

        [Theory]
        [MemberData(nameof(InstanceQuestionsWithChoices))]
        public void ChangeOptions_ForTooFew_IsNotPossible(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new();

            QuestionWithChoices question = createQuestionDelegate(data) as QuestionWithChoices;

            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(() => question.ChangeOptions(QuestionOptionsTooFew));
        }

        [Theory]
        [MemberData(nameof(InstanceQuestionsWithChoices))]
        public void ChangeOptions_When_NumberOfOptions_WithinRange_IsOk(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            QuestionTestData data = new();

            QuestionWithChoices question = createQuestionDelegate(data) as QuestionWithChoices;

            List<QuestionOption> newOptions = QuestionOptionsMinimum;
            question.ChangeOptions(QuestionOptionsMinimum);
            Assert.Equal(newOptions.Count, question.Options.Count);
        }



        #region Тестовые данные
        // ToDo разобрать все тестовые данные
        public static IEnumerable<object[]> InstanceQuestionsWithChoices =>
            new List<object[]>
            {
                new object[] { CreateSingleChoiceQuestion },
                new object[] { CreateMultiChoiceQuestion }                
            };
        #endregion
    }
}
