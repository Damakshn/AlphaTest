using AlphaTest.Core.Tests;
using AlphaTest.TestingHelpers;
using Moq;
using System.Reflection;
using System;

namespace AlphaTest.Core.UnitTests.Common.Helpers
{
    public static class HelpersForTests
    {
        // ToDo заменить все GetDefaultTest на вызов этого метода
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

        public static void SetNewStatusForTest(Test test, TestStatus status)
        {
            if (test is null)
                throw new ArgumentNullException(nameof(test));
            if (status is null)
                throw new ArgumentNullException(nameof(status));
            var property = test.GetType().GetProperty("Status", BindingFlags.Public | BindingFlags.Instance);
            if (property is null)
                throw new InvalidOperationException($"Свойство Status не найдено у типа {test.GetType()}.");
            property.SetValue(test, status, null);
        }
    }
}
