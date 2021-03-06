using AlphaTest.Core.Tests;
using AutoFixture;
using System;

namespace AlphaTest.Core.UnitTests.Fixtures.Tests
{
    internal class DefaultTestCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Test>(composer =>
               composer.FromFactory(
                   (string title, string topic, Guid authorID) =>
                   {
                    var test = new Test(title, topic, authorID, false);
                    return test;
                   })
                   
            );
            fixture.Freeze<Test>();
        }
    }
}
