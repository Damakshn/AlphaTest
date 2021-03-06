using AlphaTest.Core.Common.Abstractions;
using System;

namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionOption: Entity
    {
        public Guid ID { get; private set; }

        public Guid QuestionID { get; private set; }

        public uint Number { get; private set; }

        public string Text { get; private set; }

        public bool IsRight { get; private set; }

        private QuestionOption()
        {

        }

        public QuestionOption(Guid questionID, string text, uint number, bool isRight)
        {
            ID = Guid.NewGuid();
            QuestionID = questionID;
            Text = text;
            Number = number;
            IsRight = isRight;
        }
    }
}
