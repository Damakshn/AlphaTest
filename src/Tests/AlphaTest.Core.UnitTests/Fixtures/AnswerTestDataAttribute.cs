using AutoFixture;
using AutoFixture.Xunit2;


namespace AlphaTest.Core.UnitTests.Fixtures
{
    internal class AnswerTestDataAttribute: AutoDataAttribute
    {
        public AnswerTestDataAttribute() 
            :base(() => new Fixture().Customize(new AnswerTestingCustomization()))
        {

        }
    }
}
