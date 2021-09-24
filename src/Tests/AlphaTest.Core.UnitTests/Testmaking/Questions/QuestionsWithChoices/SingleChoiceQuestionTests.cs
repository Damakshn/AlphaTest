using System.Linq;
using System.Collections.Generic;
using Xunit;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;
using AlphaTest.Core.UnitTests.Common;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions.QuestionsWithChoices
{
    public class SingleChoiceQuestionTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(QuestionWithChoicesTestSamples.OptionsData_NoneOrManyRight), MemberType = typeof(QuestionWithChoicesTestSamples))]
        public void CreateSingleChoiceQuestion_WithNoneOrMultipleRightOptions_IsNotPossible(List<(string text, uint number, bool isRight)> optionsData)
        {            
            QuestionTestData data = new() { OptionsData = optionsData };
            
            AssertBrokenRule<ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule>(() =>
                data.Test.AddSingleChoiceQuestion(data.Text, data.Score, data.OptionsData, data.NumberOfQuestionInTest)
            );
        }

        [Fact]
        public void CreateSingleChoiceQuestion_WithExactlyOneRightOption_IsOk()
        {
            QuestionTestData data = new() { OptionsData = QuestionWithChoicesTestSamples.OptionsDataOneRight };

            SingleChoiceQuestion question = data.Test.AddSingleChoiceQuestion(data.Text, data.Score, data.OptionsData, data.NumberOfQuestionInTest);

            Assert.Equal(1, question.Options.Count(o => o.IsRight));
            Assert.Equal(data.Score, question.Score);
        }
    }
}
