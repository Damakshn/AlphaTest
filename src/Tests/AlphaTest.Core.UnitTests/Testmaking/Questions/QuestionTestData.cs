using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.TestingHelpers;
using Moq;
using System.Linq;
using System.Collections.Generic;


namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionTestData
    {
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

        // ToDo вынести в другой класс, так как эти тестовые данные - про вопросы, а не про тесты
        public static Test GetDefaultTest()
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
    }
}