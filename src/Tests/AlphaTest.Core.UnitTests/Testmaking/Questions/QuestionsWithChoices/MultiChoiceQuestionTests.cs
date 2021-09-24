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
            QuestionTestData data = new() { OptionsData = QuestionWithChoicesTestSamples.OptionsDataNoneRight };

            AssertBrokenRule<AtLeastOneQuestionOptionMustBeRightRule>(() =>
                data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.OptionsData, data.NumberOfQuestionInTest)
            );
        }

        [Theory]
        [MemberData(nameof(QuestionWithChoicesTestSamples.OptionsData_OneOrManyRight), MemberType = typeof(QuestionWithChoicesTestSamples))]
        public void CreateMultiChoiceQuestion_WithOneOrManyRightOptions_IsOk(List<(string text, uint number, bool isRight)> optionsData)
        {
            QuestionTestData data = new() { OptionsData = optionsData };

            MultiChoiceQuestion question = data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.OptionsData, data.NumberOfQuestionInTest);

            Assert.Equal(data.Test.ID, question.TestID);
            Assert.Equal(data.Score, question.Score);
            Assert.Equal(optionsData.Count, question.Options.Count);
        }
    }
}
