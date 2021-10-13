using AutoFixture;
using AutoFixture.Xunit2;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class AttemptTestsDataAttribute: AutoDataAttribute
    {
        public AttemptTestsDataAttribute()
            :base(() => new Fixture().Customize(new AttemptTestingCustomization()))
        {

        }
    }

    internal class AttemptTestsMemberDataAttribute : MemberAutoDataAttribute
    {
        public AttemptTestsMemberDataAttribute(string memberName, params object[] parameters)
            :base(new AttemptTestsDataAttribute(), memberName, parameters)
        {

        }
    }


}
