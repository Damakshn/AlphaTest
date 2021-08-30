﻿using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.Questions.Rules;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.Tests.TestSettings.Checking;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionWithDetailedAnswerTests: UnitTestBase
    {   
        [Fact]
        public void CreateQuestionWithDetailedAnswer_WhenCheckingMethodIsAutomatic_IsNotPossible()
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            test.ChangeWorkCheckingMethod(WorkCheckingMethod.AUTOMATIC, new List<Question>());
            var counterMock = new Mock<IQuestionCounter>();
            QuestionText questionText = new("Текст вопроса");
            QuestionScore score = new(1);
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            Action act = () => test.AddQuestionWithDetailedAnswer(questionText, score, counterMock.Object);

            // assert
            AssertBrokenRule<QuestionsWithDetailedAnswersNotAllowedWithAutomatedCheckRule>(act);
        }

        [Theory]
        [MemberData(nameof(QuestionTestData.NonAutomaticCheckingMethods), MemberType = typeof(QuestionTestData))]
        public void CreateQuestionWithDetailedAnswer_WhenCheckingMethodNotAutomatic_IsOk(WorkCheckingMethod checkingMethod)
        {
            // arrange
            Test test = QuestionTestData.GetDefaultTest();
            test.ChangeWorkCheckingMethod(checkingMethod, new List<Question>());
            var counterMock = new Mock<IQuestionCounter>();
            QuestionText questionText = new("Текст вопроса");
            QuestionScore score = new(1);
            counterMock.Setup(c => c.GetNumberOfQuestionsInTest(test.ID)).Returns(0);

            // act
            QuestionWithDetailedAnswer question = test.AddQuestionWithDetailedAnswer(questionText, score, counterMock.Object);

            // assert
            Assert.Equal(test.ID, question.TestID);
            Assert.Equal(score, question.Score);
        }
    }
}
