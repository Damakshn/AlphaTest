using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.UnitTests.Common.Helpers;
using AutoFixture;
using System.Collections.Generic;

namespace AlphaTest.Core.UnitTests.Fixtures.Questions
{
    internal class DefaultMultiChoiceQuestionCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<MultiChoiceQuestion>(c =>
                c.FromFactory(
                    (
                        Test test,
                        QuestionText text,
                        uint number,
                        List<(string text, uint number, bool isRight)> optionsData) => 
                {
                    HelpersForTests.SetNewStatusForTest(test, TestStatus.Draft);
                    return test.AddMultiChoiceQuestion(text, new(1), optionsData, number);
                }
            ));
        }
    }
}
