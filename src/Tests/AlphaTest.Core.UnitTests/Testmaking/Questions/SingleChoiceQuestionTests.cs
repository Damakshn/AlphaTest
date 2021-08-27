using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Moq;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;
using AlphaTest.Core.UnitTests.Common;
using static System.Formats.Asn1.AsnWriter;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class SingleChoiceQuestionTests: UnitTestBase
    {
        [Theory]
        [MemberData(nameof(QuestionTestData.Options_NoneOrManyRight), MemberType = typeof(QuestionTestData))]
        public void CreateSingleChoiceQuestion_WithNoneOrMultipleRightOptions_IsNotPossible(List<QuestionOption> options)
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            string questionText = "Кто проживает на дне океана?";
            uint score = 1;
            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            Action act = () => t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            AssertBrokenRule<ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule>(act);
        }

        [Fact]
        public void CreateSingleChoiceQuestion_WithExactlyOneRightOption_IsOk()
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            string questionText = "Кто проживает на дне океана?";
            uint score = 1;
            var questionCounterMock = new Mock<IQuestionCounter>();
            List<QuestionOption> options = QuestionTestData.QuestionOptionsOneRight;
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            SingleChoiceQuestion question = t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            Assert.Equal(1, question.Options.Count(o => o.IsRight));
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.Options_QuantityOutOfRange), MemberType = typeof(QuestionTestData))]
        public void CreateSingleChoiceQuestion_WhenOptionsLimitIsBroken_IsNotPossible(List<QuestionOption> options)
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            string questionText = "Кто проживает на дне океана?";
            uint score = 1;
            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            Action act = () => t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            AssertBrokenRule<NumberOfOptionsForQiestionMustBeInRangeRule>(act);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.Options_QuantityWithinRange), MemberType = typeof(QuestionTestData))]
        public void CreateSingleChoiceQuestion_WhenOptionsLimitIsNotBroken_IsOk(List<QuestionOption> options)
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            string questionText = "Кто проживает на дне океана?";
            uint score = 1;
            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            SingleChoiceQuestion question = t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            Assert.Equal(question.Options.Count, options.Count);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(101)]
        public void CreateSingleChoiceQuestion_WhenQuestionScoreOutOfRange_IsNotPossible(uint score)
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            string questionText = "Кто проживает на дне океана?";
            var questionCounterMock = new Mock<IQuestionCounter>();
            List<QuestionOption> options = QuestionTestData.QuestionOptionsOneRight;
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            Action act = () => t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            AssertBrokenRule<QuestionScoreMustBeInRangeRule>(act);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(85)]
        [InlineData(100)]
        public void CreateSingleChoiceQuestion_WhenQuestionScoreWithinRange_IsOk(uint score)
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            string questionText = "Кто проживает на дне океана?";
            var questionCounterMock = new Mock<IQuestionCounter>();
            List<QuestionOption> options = QuestionTestData.QuestionOptionsOneRight;
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);

            // act
            SingleChoiceQuestion question = t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            Assert.Equal(question.Score, score);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.QuestionTexts_LengthOutOfRange), MemberType = typeof(QuestionTestData))]
        public void CreateSingleChoiceQuestion_WithTooBigOrToShortText_IsNotPossible(string questionText)
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);
            List<QuestionOption> options = QuestionTestData.QuestionOptionsOneRight;
            uint score = 1;

            // act
            Action act = () => t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            AssertBrokenRule<QuestionTextLengthMustBeInRangeRule>(act);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.QuestionTexts_LengthWithinRange), MemberType = typeof(QuestionTestData))]
        public void CreateSingleChoiceQuestion_WhenTextLengthWithinRange_IsOk(string questionText)
        {
            // arrange
            Test t = QuestionTestData.GetDefaultTest();
            var questionCounterMock = new Mock<IQuestionCounter>();
            questionCounterMock.Setup(qc => qc.GetNumberOfQuestionsInTest(It.IsAny<int>())).Returns(0);
            List<QuestionOption> options = QuestionTestData.QuestionOptionsOneRight;
            uint score = 1;

            // act
            SingleChoiceQuestion question = t.AddSingleChoiceQuestion(questionText, score, options, questionCounterMock.Object);

            // assert
            Assert.Equal(questionText, question.Text);
        }
    }
}
