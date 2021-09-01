﻿using System.Linq;
using System.Collections.Generic;
using Xunit;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class SingleChoiceQuestionTests: QuestionWithChoicesTestsBase
    {
        [Theory]
        [MemberData(nameof(Options_NoneOrManyRight))]
        public void CreateSingleChoiceQuestion_WithNoneOrMultipleRightOptions_IsNotPossible(List<QuestionOption> options)
        {            
            QuestionWithChoicesTestData data = new() { Options = options };
            
            AssertBrokenRule<ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule>(() =>
                data.Test.AddSingleChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object)
            );
        }

        [Fact]
        public void CreateSingleChoiceQuestion_WithExactlyOneRightOption_IsOk()
        {
            QuestionWithChoicesTestData data = new() { Options = QuestionOptionsOneRight };

            SingleChoiceQuestion question = data.Test.AddSingleChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

            Assert.Equal(1, question.Options.Count(o => o.IsRight));
            Assert.Equal(data.Score, question.Score);
        }

        [Theory]
        [MemberData(nameof(Options_QuantityOutOfRange))]
        public void CreateSingleChoiceQuestion_WhenOptionsLimitIsBroken_IsNotPossible(List<QuestionOption> options)
        {
            QuestionWithChoicesTestData data = new() { Options = options };

            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(() =>
                data.Test.AddSingleChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object)
            );
        }

        [Theory]
        [MemberData(nameof(Options_QuantityWithinRange))]
        public void CreateSingleChoiceQuestion_WhenOptionsLimitIsNotBroken_IsOk(List<QuestionOption> options)
        {
            QuestionWithChoicesTestData data = new() { Options = options };

            SingleChoiceQuestion question = data.Test.AddSingleChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

            Assert.Equal(question.Options.Count, options.Count);
            Assert.Equal(data.Score, question.Score);
        }

        [Fact]
        public void WhenCreateSingleChoiceQuestion_WithUnifiedScore_ScoreFromUserIsIgnored()
        {
            QuestionScore unifiedScore = new(10);
            QuestionScore userScore = new(5);
            QuestionWithChoicesTestData data = new() { Score = userScore, Options = QuestionOptionsOneRight };
            data.Test.ChangeScoreDistributionMethod(ScoreDistributionMethod.UNIFIED);
            data.Test.ChangeScorePerQuestion(unifiedScore);

            SingleChoiceQuestion question = data.Test.AddSingleChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

            Assert.Equal(unifiedScore, question.Score);
            Assert.NotEqual(userScore, question.Score);
        }
    }
}
