using AutoFixture;
using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class TestmakingTestsDataAttribute : AutoDataAttribute
    {
        public TestmakingTestsDataAttribute()
            :base(() => new Fixture().Customize(new TestmakingTestsCustomization()))
        {

        }
    }
}
