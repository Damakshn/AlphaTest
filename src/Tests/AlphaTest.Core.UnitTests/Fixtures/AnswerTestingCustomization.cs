using AutoFixture;
using AlphaTest.Core.Users;
using AlphaTest.Core.UnitTests.Fixtures.Tests;
using AlphaTest.Core.UnitTests.Fixtures.Questions;
using AlphaTest.Core.UnitTests.Fixtures.Examinations;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class AnswerTestingCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {   
            fixture.Customize<UserRole>(c =>
                c.FromFactory(() => UserRole.TEACHER)
            );
            fixture.Customizations.Add(new OptionsDataDefaultSpecimenBuilder());
            fixture.Customize(new DefaultTestCustomization());
            fixture.Customize(new DefaultSingleChoiceQuestionCustomization());
            fixture.Customize(new DefaultMultiChoiceQuestionCustomization());
            fixture.Customize(new DefaultQuestionWithNumericAnswerCustomization());
            fixture.Customize(new DefaultQuestionWithTextualAnswerCustomization());
            fixture.Customize(new DefaultQuestionWithDetailedAnswerCustomization());
            fixture.Customize(new DefaultExaminationCustomization());
        }
    }
}
