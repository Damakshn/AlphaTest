using System.Collections.Generic;
using AutoFixture;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.UnitTests.Common.Helpers;

namespace AlphaTest.Core.UnitTests.Fixtures.Questions
{
    internal class DefaultSingleChoiceQuestionCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<SingleChoiceQuestion>(c =>
                c.FromFactory(
                    (Test test, QuestionText text, uint number, List<(string text, uint number, bool isRight)> optionsData) =>
                    {
                        HelpersForTests.SetNewStatusForTest(test, TestStatus.Draft);
                        return test.AddSingleChoiceQuestion(text, new QuestionScore(1), optionsData, number);
                    }
            ));
        }
    }
}
