using AutoFixture;
using AlphaTest.Core.UnitTests.Fixtures.Tests;
using AlphaTest.Core.UnitTests.Fixtures.Questions;
using AlphaTest.Core.UnitTests.Fixtures.Examinations;
using AlphaTest.Core.UnitTests.Fixtures.Answers;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class AnswerTestingCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customizations.Add(new OptionsDataDefaultSpecimenBuilder());
            fixture.Customize(new DefaultTestCustomization());
            fixture.Customize(new DefaultSingleChoiceQuestionCustomization());
            fixture.Customize(new DefaultMultiChoiceQuestionCustomization());
            fixture.Customize(new DefaultQuestionWithNumericAnswerCustomization());
            fixture.Customize(new DefaultQuestionWithTextualAnswerCustomization());
            fixture.Customize(new DefaultQuestionWithDetailedAnswerCustomization());
            fixture.Customize(new DefaultExaminationCustomization());
            fixture.Customize(new DefaultMultiChoiceAnswerCustomization());
            fixture.Customize(new DefaultSingleChoiceAnswerCustomization());
        }
    }
}
