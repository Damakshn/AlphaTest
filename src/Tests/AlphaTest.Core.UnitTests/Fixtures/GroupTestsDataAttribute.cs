using AutoFixture;
using AutoFixture.Xunit2;
using AlphaTest.Core.UnitTests.Fixtures.Groups;
using System;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class GroupTestsDataAttribute: AutoDataAttribute
    {
        public GroupTestsDataAttribute()
            : base(() => new Fixture().Customize(new DefaultGroupCustomization()))
        {

        }
    }


    internal class GroupTestsMemberDataAttribute : MemberAutoDataAttribute
    {
        public GroupTestsMemberDataAttribute(string memberName, params object[] parameters)
            : base(new GroupTestsDataAttribute(), memberName, parameters)
        {

        }
    }

    internal class GroupTestsInlineDataAttribute : InlineAutoDataAttribute
    {
        public GroupTestsInlineDataAttribute(params object[] values)
            :base(new GroupTestsDataAttribute(), values)
        {
                
        }
    }

    
}
