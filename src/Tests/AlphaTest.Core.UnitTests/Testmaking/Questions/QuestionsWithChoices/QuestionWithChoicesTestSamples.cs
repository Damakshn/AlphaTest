using AlphaTest.Core.Tests.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions.QuestionsWithChoices
{
    public class QuestionWithChoicesTestSamples: QuestionTestSamples
    {
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

        public static List<QuestionOption> QuestionOptionsTooMany =>
            Enumerable.Range(1, 21)
                .Select(x => new QuestionOption($"{x}-й вариант", (uint)x, x == 1))
                .ToList();

        public static List<QuestionOption> QuestionOptionsTooFew =>
            new List<QuestionOption>
            {
                new QuestionOption("Единственный вариант", 1, false)
            };

        public static List<QuestionOption> QuestionOptionsMinimum =>
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

        public static IEnumerable<object[]> InstanceQuestionsWithChoices =>
            new List<object[]>
            {
                new object[] { CreateSingleChoiceQuestion },
                new object[] { CreateMultiChoiceQuestion }
            };
    }
}
