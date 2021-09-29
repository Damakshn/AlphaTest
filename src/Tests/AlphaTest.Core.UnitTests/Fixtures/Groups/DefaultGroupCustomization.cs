using System;
using AutoFixture;
using AlphaTest.Core.Groups;


namespace AlphaTest.Core.UnitTests.Fixtures.Groups
{
    internal class DefaultGroupCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Group>(
                c => c.FromFactory(
                    (string groupName) => new Group(groupName, DateTime.Now.AddDays(1), DateTime.Now.AddDays(100), false)
                )
            );
        }
    }
}
