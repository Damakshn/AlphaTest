using AutoFixture;
using AutoFixture.Xunit2;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class WorkTestsDataAttribute: AutoDataAttribute
    {
        public WorkTestsDataAttribute()
            :base(() => new Fixture().Customize(new WorkTestingCustomization()))
        {

        }
    }

    internal class WorkTestsMemberDataAttribute : MemberAutoDataAttribute
    {
        public WorkTestsMemberDataAttribute(string memberName, params object[] parameters)
            :base(new WorkTestsDataAttribute(), memberName, parameters)
        {

        }
    }


}
