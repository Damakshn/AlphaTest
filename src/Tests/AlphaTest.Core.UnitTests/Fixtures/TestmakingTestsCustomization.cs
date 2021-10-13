using AutoFixture;
using AlphaTest.Core.UnitTests.Fixtures.Tests;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class TestmakingTestsCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {   
            fixture.Customize(new DefaultTestCustomization());
        }
    }
}
