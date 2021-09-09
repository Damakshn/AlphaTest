using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionOption: Entity
    {
        public int ID { get; private set; }

        public uint Number { get; private set; }

        public string Text { get; private set; }

        public bool IsRight { get; private set; }

        private QuestionOption()
        {

        }

        public QuestionOption(string text, uint number, bool isRight)
        {
            Text = text;
            Number = number;
            IsRight = isRight;
        }
    }
}
