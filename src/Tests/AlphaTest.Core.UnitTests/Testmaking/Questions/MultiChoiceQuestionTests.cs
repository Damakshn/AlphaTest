using System.Collections.Generic;
using Xunit;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class MultiChoiceQuestionTests: QuestionWithChoicesTestsBase
    {
        [Fact]
        public void CreateMultiChoiceQuestion_WithoutRightOptions_IsNotPossible()
        {
            QuestionWithChoicesTestData data = new() { Options = QuestionOptionsNoneRight };

            AssertBrokenRule<AtLeastOneQuestionOptionMustBeRightRule>(() =>
                data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object)
            );
        }

        [Theory]
        [MemberData(nameof(Options_OneOrManyRight))]
        public void CreateMultiChoiceQuestion_WithOneOrManyRightOptions_IsOk(List<QuestionOption> options)
        {
            QuestionWithChoicesTestData data = new() { Options = options };

            MultiChoiceQuestion question = data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

            Assert.Equal(data.Test.ID, question.TestID);
            Assert.Equal(data.Score, question.Score);
            Assert.Equal(options.Count, question.Options.Count);
        }

        [Theory]
        [MemberData(nameof(Options_QuantityOutOfRange))]
        public void CreateMultiChoiceQuestion_WhenOptionsLimitIsBroken_IsNotPossible(List<QuestionOption> options)
        {
            QuestionWithChoicesTestData data = new() { Options = options };

            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(() =>
                data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object)
            );
        }

        [Theory]
        [MemberData(nameof(Options_QuantityWithinRange))]
        public void CreateMultiChoiceQuestion_WithOptionsNumberWithinRange_IsOk(List<QuestionOption> options)
        {
            QuestionWithChoicesTestData data = new() { Options = options };

            MultiChoiceQuestion question = data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

            Assert.Equal(data.Test.ID, question.TestID);
            Assert.Equal(options.Count, question.Options.Count);
        }
    }
}
