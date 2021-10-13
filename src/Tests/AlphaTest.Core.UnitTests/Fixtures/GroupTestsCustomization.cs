using AutoFixture;
using AlphaTest.Core.UnitTests.Fixtures.Groups;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class GroupTestsCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {   
            fixture.Customize(new DefaultGroupCustomization());
        }
    }
}
