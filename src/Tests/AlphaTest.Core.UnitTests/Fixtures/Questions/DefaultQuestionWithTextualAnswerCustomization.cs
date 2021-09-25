using AutoFixture;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests;
using AlphaTest.Core.UnitTests.Common.Helpers;

namespace AlphaTest.Core.UnitTests.Fixtures.Questions
{
    internal class DefaultQuestionWithTextualAnswerCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<QuestionWithTextualAnswer>(c =>
                c.FromFactory(
                    (
                        Test test,
                        QuestionText text,
                        uint number) =>
                    {
                        HelpersForTests.SetNewStatusForTest(test, TestStatus.Draft);
                        return test.AddQuestionWithTextualAnswer(text, new(1), "ПравильныйОтвет", number);
                    }
            ));
        }
    }
}
