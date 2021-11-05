using AlphaTest.Core.UnitTests.Fixtures.Examinations;
using AlphaTest.Core.UnitTests.Fixtures.Tests;
using AutoFixture;


namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class WorkTestingCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize(new DefaultTestCustomization());
            fixture.Customize(new DefaultExaminationCustomization());
        }
    }
}
