using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests
{
    public class TestStatus: Enumeration<TestStatus>
    {
        public TestStatus(int id, string name):base(id, name) { }

        public static readonly TestStatus Draft = new(1, "Черновик");

        public static readonly TestStatus WaitingForPublishing = new(2, "В ожидании публикации");

        public static readonly TestStatus Published = new TestStatus(3, "Опубликован");

        public static readonly TestStatus Archived = new TestStatus(4, "В архиве");
    }
}
