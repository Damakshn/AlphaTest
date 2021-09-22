using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AutoFixture;
using System.Collections.Generic;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class DefaultMultiChoiceQuestionCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            List<QuestionOption> options = new()
            {
                new QuestionOption("Первый вариант", 1, true),
                new QuestionOption("Второй вариант", 2, true),
                new QuestionOption("Третий вариант", 3, false)
            };
            fixture.Customize<MultiChoiceQuestion>(c =>
                c.FromFactory((Test test, QuestionText text, uint number) => 
                {
                    HelpersForTests.SetNewStatusForTest(test, TestStatus.Draft);
                    return test.AddMultiChoiceQuestion(text, new(1), options, number);
                }
            ));
        }
    }
}
