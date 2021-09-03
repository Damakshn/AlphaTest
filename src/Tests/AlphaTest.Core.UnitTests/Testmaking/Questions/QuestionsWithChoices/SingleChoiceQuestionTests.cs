using System.Linq;
using System.Collections.Generic;
using Xunit;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions.QuestionsWithChoices
{
    public class SingleChoiceQuestionTests: QuestionTestsBase
    {
        [Theory]
        [MemberData(nameof(Options_NoneOrManyRight))]
        public void CreateSingleChoiceQuestion_WithNoneOrMultipleRightOptions_IsNotPossible(List<QuestionOption> options)
        {            
            QuestionTestData data = new() { Options = options };
            
            AssertBrokenRule<ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule>(() =>
                data.Test.AddSingleChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object)
            );
        }

        [Fact]
        public void CreateSingleChoiceQuestion_WithExactlyOneRightOption_IsOk()
        {
            QuestionTestData data = new() { Options = QuestionOptionsOneRight };

            SingleChoiceQuestion question = data.Test.AddSingleChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

            Assert.Equal(1, question.Options.Count(o => o.IsRight));
            Assert.Equal(data.Score, question.Score);
        }
    }
}
