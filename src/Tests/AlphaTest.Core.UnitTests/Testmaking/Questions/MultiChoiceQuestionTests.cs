using System.Collections.Generic;
using Xunit;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class MultiChoiceQuestionTests: QuestionTestsBase
    {
        [Fact]
        public void CreateMultiChoiceQuestion_WithoutRightOptions_IsNotPossible()
        {
            QuestionTestData data = new() { Options = QuestionOptionsNoneRight };

            AssertBrokenRule<AtLeastOneQuestionOptionMustBeRightRule>(() =>
                data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object)
            );
        }

        [Theory]
        [MemberData(nameof(Options_OneOrManyRight))]
        public void CreateMultiChoiceQuestion_WithOneOrManyRightOptions_IsOk(List<QuestionOption> options)
        {
            QuestionTestData data = new() { Options = options };

            MultiChoiceQuestion question = data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

            Assert.Equal(data.Test.ID, question.TestID);
            Assert.Equal(data.Score, question.Score);
            Assert.Equal(options.Count, question.Options.Count);
        }

        [Theory]
        [MemberData(nameof(Options_QuantityOutOfRange))]
        public void CreateMultiChoiceQuestion_WhenOptionsLimitIsBroken_IsNotPossible(List<QuestionOption> options)
        {
            QuestionTestData data = new() { Options = options };

            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(() =>
                data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object)
            );
        }

        [Theory]
        [MemberData(nameof(Options_QuantityWithinRange))]
        public void CreateMultiChoiceQuestion_WithOptionsNumberWithinRange_IsOk(List<QuestionOption> options)
        {
            QuestionTestData data = new() { Options = options };

            MultiChoiceQuestion question = data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

            Assert.Equal(data.Test.ID, question.TestID);
            Assert.Equal(options.Count, question.Options.Count);
        }
    }
}
