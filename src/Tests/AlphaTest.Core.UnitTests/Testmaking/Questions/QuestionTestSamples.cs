using AlphaTest.Core.Tests;
using AlphaTest.TestingHelpers;
using Moq;
using System.Linq;
using System.Collections.Generic;
using AlphaTest.Core.Tests.TestSettings.Checking;

namespace AlphaTest.Core.UnitTests.Testmaking.Questions
{
    public class QuestionTestSamples
    {

        public static IEnumerable<object[]> NonAutomaticCheckingMethods =>
            WorkCheckingMethod.All
            .Where(m => m != WorkCheckingMethod.AUTOMATIC)
            .Select(m => new object[] { m })
            .ToList();

        // ToDo вынести в другой класс, так как эти тестовые данные - про вопросы, а не про тесты
        public static Test GetDefaultTest()
        {
            string title = It.IsAny<string>();
            string topic = It.IsAny<string>();
            int authorID = It.IsAny<int>();
            var testCounterMock = new Mock<ITestCounter>();
            testCounterMock
                .Setup(
                    c => c.GetQuantityOfTests(title, topic, Test.INITIAL_VERSION, authorID)
                )
                .Returns(0);
            Test defaultTest = new(title, topic, authorID, testCounterMock.Object);
            EntityIDSetter.SetIDTo(defaultTest, 1);
            return defaultTest;
        }
        
    }
}