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
        public static IEnumerable<object[]> OptionsData_NoneOrManyRight =>
            new List<object[]>
            {
                new object[] { OptionsDataManyRight},
                new object[] { OptionsDataNoneRight}
            };

        public static IEnumerable<object[]> OptionsData_OneOrManyRight =>
            new List<object[]>
            {
                new object[]{OptionsDataOneRight},
                new object[]{OptionsDataManyRight}
            };
        
        public static List<(string text, uint number, bool isRight)> OptionsDataOneRight =>
            new List<(string text, uint number, bool isRight)>
            {
                new ("Первый вариант", 1, true),
                new ("Второй вариант", 2, false),
                new ("Третий вариант", 3, false),
            };

        public static List<(string text, uint number, bool isRight)> OptionsDataManyRight =>
            new List<(string text, uint number, bool isRight)>
            {
                new ("Первый вариант", 1, true),
                new ("Второй вариант", 2, true),
                new ("Третий вариант", 3, false),
            };

        public static List<(string text, uint number, bool isRight)> OptionsDataNoneRight =>
            new List<(string text, uint number, bool isRight)>
            {
                new ("Первый вариант", 1, false),
                new ("Второй вариант", 2, false),
                new ("Третий вариант", 3, false),
            };
        
        public static List<(string text, uint number, bool isRight)> OptionsDataTooMany =>
            Enumerable.Range(1, 21)
                .Select(x => ($"{x}-й вариант", (uint)x, x == 1))
                .ToList();

        public static List<(string text, uint number, bool isRight)> OptionsDataTooFew =>
            new List<(string text, uint number, bool isRight)>
            {
                new ("Единственный вариант", 1, false)
            };
        

        public static List<(string text, uint number, bool isRight)> OptionsDataMinimum =>
            new List<(string text, uint number, bool isRight)>
            {
                new ("Первый вариант", 1, true),
                new ("Второй вариант", 2, false)
            };
        #endregion
    }
}
