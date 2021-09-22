using System.Collections.Generic;
using AutoFixture;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.UnitTests.Common.Helpers;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class DefaultSingleChoiceQuestionCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            List<QuestionOption> options = new()
            {
                new QuestionOption("Первый вариант", 1, true),
                new QuestionOption("Второй вариант", 2, false),
                new QuestionOption("Третий вариант", 3, false)
            };
            fixture.Customize<SingleChoiceQuestion>(c =>
                c.FromFactory(
                    (Test test, QuestionText text, uint number) =>
                    {
                        HelpersForTests.SetNewStatusForTest(test, TestStatus.Draft);
                        return test.AddSingleChoiceQuestion(text, new QuestionScore(1), options, number);
                    }
            ));
        }
    }
}
