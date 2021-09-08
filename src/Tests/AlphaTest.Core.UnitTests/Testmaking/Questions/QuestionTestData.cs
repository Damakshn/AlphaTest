using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests;
using Moq;
using System;
using System.Collections.Generic;
using AlphaTest.TestingHelpers;
using AlphaTest.Core.UnitTests.Common.Helpers;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionTestData
    {
        public QuestionTestData()
        {
            
        }

        internal Test Test { get; set; } = HelpersForTests.GetDefaultTest();

        internal QuestionScore Score { get; set; } = GetRandomScore();

        internal QuestionText Text { get; set; } = new("Произвольный текст вопроса");

        internal string TextualAnswer { get; set; } = "Правильный ответ";

        internal decimal NumericAnswer { get; set; } = 1789;

        internal List<QuestionOption> Options { get; set; } = new List<QuestionOption>
            {
                new QuestionOption("Первый вариант", 1, true),
                new QuestionOption("Второй вариант", 2, false),
                new QuestionOption("Третий вариант", 3, false),
            };

        internal uint NumberOfQuestionInTest { get; set; } = 0;

        public static QuestionScore GetRandomScore()
        {
            Random random = new();
            return new QuestionScore(
                random.Next(
                    1,
                    100
                )
            );
        }
    }
}
