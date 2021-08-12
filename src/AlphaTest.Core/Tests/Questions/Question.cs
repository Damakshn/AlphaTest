using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class Question: Entity
    {
        public int ID { get; init; }

        public string Text { get; set; }

        public int Number { get; set; }

        public uint Score { get; set; }

        public Question()
        {

        }

    }
}
