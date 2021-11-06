﻿using AlphaTest.Core.Works;
using AlphaTest.Core.Tests.Questions;

namespace AlphaTest.Core.Answers
{
    public class ExactTextualAnswer: Answer
    {
        private ExactTextualAnswer() : base() { }

        public ExactTextualAnswer(QuestionWithTextualAnswer question, Work work, string value)
            : base(work, question)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
