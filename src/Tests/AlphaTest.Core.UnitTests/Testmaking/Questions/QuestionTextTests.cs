using System;
using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;
using System.Collections.Generic;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionTextTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(QuestionTexts_LengthOutOfRange))]
        public void CreateQuestionText_WhenLenghtIsOutOfRange_IsNotPossible(string value)
        {
            Action act = () => new QuestionText(value);
            AssertBrokenRule<QuestionTextLengthMustBeInRangeRule>(act);
        }

        [Theory]
        [MemberData(nameof(QuestionTexts_LengthWithinRange))]
        public void CreateQuestionText_WhenLengthWithinRange_IsOk(string value)
        {
            QuestionText questionText = new(value);
            Assert.Equal(value, questionText.Value);
        }

        public static IEnumerable<object[]> QuestionTexts_LengthOutOfRange =>
            new List<object[]>
            {
                new object[] {""},
                new object[] {"a"},
                new object[] {new string('a',5)},
                new object[] {new string('a',9)},
                new object[] {new string('a',5001)}
            };

        public static IEnumerable<object[]> QuestionTexts_LengthWithinRange =>
            new List<object[]>
            {
                new object[] {new string('a',10)},
                new object[] {new string('a',250)},
                new object[] {new string('a',5000)}
            };


    }
}
