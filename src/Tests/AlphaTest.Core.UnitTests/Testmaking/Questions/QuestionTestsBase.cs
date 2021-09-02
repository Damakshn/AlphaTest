using System;
using System.Collections.Generic;
using Moq;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.UnitTests.Common;
using AlphaTest.TestingHelpers;
using System.Linq;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public abstract class QuestionTestsBase: UnitTestBase
    {
        protected static Test GetDefaultTest()
        {
            string title = It.IsAny<string>();
            string topic = It.IsAny<string>();
            int authorID = It.IsAny<int>();
            var testCounterMock = new Mock<ITestCounter>();
            testCounterMock
                .Setup(
                    c => c.GetQuantityOfTests(title, topic, Test.INITIAL_VERSION, authorID)
                )
                .Returns(0);
            Test defaultTest = new(title, topic, authorID, testCounterMock.Object);
            EntityIDSetter.SetIDTo(defaultTest, 1);
            return defaultTest;
        }

        protected static QuestionScore GetRandomScore()
        {
            Random random = new();
            return new QuestionScore(
                random.Next(
                    1,
                    100
                )
            );
        }

        public class QuestionTestData
        {
            public QuestionTestData()
            {
                CounterMock.Setup(m => m.GetNumberOfQuestionsInTest(Test.ID)).Returns(0);
            }

            internal Mock<IQuestionCounter> CounterMock = new Mock<IQuestionCounter>();

            internal Test Test { get; set; } = GetDefaultTest();

            internal QuestionScore Score { get; set; } = GetRandomScore();

            internal QuestionText Text { get; set; } = new("Произвольный текст вопроса");

            internal string TextualAnswer { get; set; } = "Правильный ответ";

            internal decimal NumericAnswer { get; set; } = 1789;

            internal List<QuestionOption> Options { get; set; } = QuestionOptionsOneRight;
        }

        #region Тестовые данные

        #region Создание вопросов со стандартными данными
        protected static Func<QuestionTestData, Question> CreateSingleChoiceQuestion =
            data => data.Test.AddSingleChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

        protected static Func<QuestionTestData, Question> CreateMultiChoiceQuestion =
            data => data.Test.AddMultiChoiceQuestion(data.Text, data.Score, data.Options, data.CounterMock.Object);

        protected static Func<QuestionTestData, Question> CreateQuestionWithTextualAnswer =
            data => data.Test.AddQuestionWithTextualAnswer(data.Text, data.Score, data.TextualAnswer, data.CounterMock.Object);

        protected static Func<QuestionTestData, Question> CreateQuestionWithNumericAnswer =
            data => data.Test.AddQuestionWithNumericAnswer(data.Text, data.Score, data.NumericAnswer, data.CounterMock.Object);

        protected static Func<QuestionTestData, Question> CreateQuestionWithDetailedAnswer =
            data => data.Test.AddQuestionWithDetailedAnswer(data.Text, data.Score, data.CounterMock.Object);

        public static IEnumerable<object[]> InstanceAllTypesOfQuestions =>
            new List<object[]>
            {
                new object[] { CreateSingleChoiceQuestion },
                new object[] { CreateMultiChoiceQuestion },
                new object[] { CreateQuestionWithTextualAnswer },
                new object[] { CreateQuestionWithNumericAnswer },
                new object[] { CreateQuestionWithDetailedAnswer }
            };

        #endregion

        #region Для вопросов с выбором вариантов ответа
        public static IEnumerable<object[]> Options_NoneOrManyRight =>
            new List<object[]>
            {
                new object[] { QuestionOptionsManyRight},
                new object[] { QuestionOptionsNoneRight}
            };

        public static IEnumerable<object[]> Options_OneOrManyRight =>
            new List<object[]>
            {
                new object[]{QuestionOptionsOneRight},
                new object[]{QuestionOptionsManyRight}
            };

        public static IEnumerable<object[]> Options_QuantityOutOfRange =>
            new List<object[]>
            {
                new object[] { QuestionOptionsTooMany },
                new object[] { QuestionOptionsTooFew }
            };

        public static IEnumerable<object[]> Options_QuantityWithinRange =>
            new List<object[]>
            {
                new object[] { QuestionOptionsMinimum },
                new object[] { QuestionOptionsMedium },
                new object[] { QuestionOptionsMaximum }
            };

        public static List<QuestionOption> QuestionOptionsOneRight =>
            new List<QuestionOption>
            {
                new QuestionOption("Первый вариант", 1, true),
                new QuestionOption("Второй вариант", 2, false),
                new QuestionOption("Третий вариант", 3, false),
            };

        public static List<QuestionOption> QuestionOptionsManyRight =>
            new List<QuestionOption>
            {
                new QuestionOption("Первый вариант", 1, true),
                new QuestionOption("Второй вариант", 2, true),
                new QuestionOption("Третий вариант", 3, false),
            };

        public static List<QuestionOption> QuestionOptionsNoneRight =>
            new List<QuestionOption>
            {
                new QuestionOption("Первый вариант", 1, false),
                new QuestionOption("Второй вариант", 2, false),
                new QuestionOption("Третий вариант", 3, false),
            };

        private static List<QuestionOption> QuestionOptionsTooMany =>
            Enumerable.Range(1, 21)
                .Select(x => new QuestionOption($"{x}-й вариант", (uint)x, x == 1))
                .ToList();

        private static List<QuestionOption> QuestionOptionsTooFew =>
            new List<QuestionOption>
            {
                new QuestionOption("Единственный вариант", 1, false)
            };

        private static List<QuestionOption> QuestionOptionsMinimum =>
            new List<QuestionOption>
            {
                new QuestionOption("Первый вариант", 1, true),
                new QuestionOption("Второй вариант", 2, false)
            };

        private static List<QuestionOption> QuestionOptionsMedium =>
            Enumerable.Range(1, 10)
                .Select(x => new QuestionOption($"{x}-й вариант", (uint)x, x == 1))
                .ToList();

        private static List<QuestionOption> QuestionOptionsMaximum =>
            Enumerable.Range(1, 20)
                .Select(x => new QuestionOption($"{x}-й вариант", (uint)x, x == 1))
                .ToList();
        #endregion

        #endregion
    }
}
