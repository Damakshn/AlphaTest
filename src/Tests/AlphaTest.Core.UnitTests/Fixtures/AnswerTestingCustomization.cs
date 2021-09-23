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
            fixture.Customize(new DefaultTestCustomization());
            fixture.Customize(new DefaultSingleChoiceQuestionCustomization());
            fixture.Customize(new DefaultMultiChoiceQuestionCustomization());
            fixture.Customize(new DefaultExaminationCustomization());
        }
    }
}
