using AutoFixture;
using AlphaTest.Core.UnitTests.Fixtures.Tests;
using AlphaTest.Core.UnitTests.Fixtures.Users;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class TestmakingTestsCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {   
            fixture.Customize(new DefaultTestCustomization());
            fixture.Customize(new IAlphaTestUserMockCustomization());
        }
    }
}
