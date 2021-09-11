﻿using System;
using Moq;
using Xunit;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using System.Collections.Generic;
using System.Linq;
using AlphaTest.TestingHelpers;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class ReplicationTests: UnitTestBase
    {   
        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceAllTypesOfQuestions), MemberType = typeof(QuestionTestSamples))]
        public void Replicated_question_has_same_text_score_and_number_as_source_question(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            // arrange
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new()
            {
                Test = test,
                Text = new("ТекстВопросаПростоЧтобыБыл"),
                Score = new(25)
            };

            Question source = createQuestionDelegate(data);

            // act
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Test newEdition = test.Replicate(It.IsAny<int>());
            Question replica = source.ReplicateForNewEdition(newEdition);

            // assert
            Assert.Equal(source.Number, replica.Number);
            Assert.Equal(source.Text, replica.Text);
            Assert.Equal(source.Score, replica.Score);
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceAllTypesOfQuestions), MemberType = typeof(QuestionTestSamples))]
        public void Replicated_question_is_bound_to_different_test_than_source_question(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            Test test = HelpersForTests.GetDefaultTest();
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            QuestionTestData data = new();
            Question source = createQuestionDelegate(data);
            
            Test newEdition = test.Replicate(It.IsAny<int>());
            Question replica = source.ReplicateForNewEdition(newEdition);

            Assert.NotEqual(test.ID, replica.TestID);
            Assert.Equal(newEdition.ID, replica.TestID);
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceAllTypesOfQuestions), MemberType = typeof(QuestionTestSamples))]
        public void Replicated_question_ID_is_reset_to_default(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            Test test = HelpersForTests.GetDefaultTest();
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            QuestionTestData data = new();
            Question source = createQuestionDelegate(data);

            Test newEdition = test.Replicate(It.IsAny<int>());
            Question replica = source.ReplicateForNewEdition(newEdition);

            Assert.Equal(default, replica.ID);
        }

        [Fact]        
        public void Replicated_question_with_numeric_answer_has_same_right_answer_as_source_question()
        {
            // arrange
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new()
            {
                Test = test,
                Text = new("В каком году был запущен первый искусственный спутник Земли?"),
                NumericAnswer = 1957,
                Score = new(10)
            };
            QuestionWithNumericAnswer source = (QuestionWithNumericAnswer)QuestionTestSamples.CreateQuestionWithNumericAnswer(data);
            
            // act
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Test newEdition = test.Replicate(It.IsAny<int>());
            QuestionWithNumericAnswer replica = source.ReplicateForNewEdition(newEdition);

            // assert
            Assert.Equal(1957, replica.RightAnswer);
        }

        [Fact]
        public void Replicated_question_with_textual_answer_has_same_right_answer_as_source_question()
        {
            // arrange
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new()
            {
                Test = test,
                Text = new("Как называется столица Гондураса?"),
                TextualAnswer = "Тегусигальпа",
                Score = new(10)
            };
            QuestionWithTextualAnswer source = (QuestionWithTextualAnswer)QuestionTestSamples.CreateQuestionWithTextualAnswer(data);

            // act
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Test newEdition = test.Replicate(It.IsAny<int>());
            QuestionWithTextualAnswer replica = source.ReplicateForNewEdition(newEdition);

            // assert
            Assert.Equal("Тегусигальпа", replica.RightAnswer);
        }

        [Theory]
        [MemberData(nameof(QuestionTestSamples.InstanceQuestionsWithChoices), MemberType = typeof(QuestionTestSamples))]
        public void Replicated_question_with_choices_has_same_set_of_options_with_ID_reset_to_default(Func<QuestionTestData, Question> createQuestionDelegate)
        {
            // arrange
            Test test = HelpersForTests.GetDefaultTest();
            QuestionTestData data = new()
            {
                Test = test,
                Options = new List<QuestionOption>
                {
                    new QuestionOption("Раз", 1, true),
                    new QuestionOption("Два", 2, false),
                    new QuestionOption("Три", 3, false),
                    new QuestionOption("Четыре", 4, false),
                    new QuestionOption("Пять", 5, false),
                }
            };
            QuestionWithChoices source = (QuestionWithChoices)createQuestionDelegate(data);
            int optionID = 1;
            foreach (var option in source.Options)
            {
                EntityIDSetter.SetIDTo(option, optionID++);
            }

            // act
            HelpersForTests.SetNewStatusForTest(test, TestStatus.Published);
            Test newEdition = test.Replicate(It.IsAny<int>());
            QuestionWithChoices replica = (QuestionWithChoices)source.ReplicateForNewEdition(newEdition);

            // assert
            foreach (var option in source.Options)
            {
                var replicatedOption = replica.Options.Where(o => o.Number == option.Number).FirstOrDefault();
                Assert.NotNull(replicatedOption);
                Assert.Equal(option.Text, replicatedOption.Text);
                Assert.Equal(option.IsRight, replicatedOption.IsRight);
                Assert.Equal(default, replicatedOption.ID);
            }
        }


    }
}
