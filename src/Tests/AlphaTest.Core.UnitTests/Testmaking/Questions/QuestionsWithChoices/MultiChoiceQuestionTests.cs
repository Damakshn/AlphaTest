using System.Collections.Generic;
using Xunit;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;
using AlphaTest.Core.UnitTests.Common;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions.QuestionsWithChoices
{
    public class MultiChoiceQuestionTests: UnitTestBase
    {
        [Fact]
        public void CreateMultiChoiceQuestion_WithoutRightOptions_IsNotPossible()
        {
            QuestionTestData data = new() { Options = QuestionWithChoicesTestSamples.QuestionOptionsNoneRight };

            AssertBrokenRule<AtLeastOneQuestionOptionMustBeRightRule>(() =>
                data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.NumberOfQuestionInTest)
            );
        }

        [Theory]
        [MemberData(nameof(QuestionWithChoicesTestSamples.Options_OneOrManyRight), MemberType = typeof(QuestionWithChoicesTestSamples))]
        public void CreateMultiChoiceQuestion_WithOneOrManyRightOptions_IsOk(List<QuestionOption> options)
        {
            QuestionTestData data = new() { Options = options };

            MultiChoiceQuestion question = data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.NumberOfQuestionInTest);

            Assert.Equal(data.Test.ID, question.TestID);
            Assert.Equal(data.Score, question.Score);
            Assert.Equal(options.Count, question.Options.Count);
        }
    }
}
