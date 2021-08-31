﻿namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithTextualAnswer: QuestionWithExactAnswer<string>
    {
        private QuestionWithTextualAnswer() { }

        internal QuestionWithTextualAnswer(int testID, QuestionText text, uint number, QuestionScore score, string rightAnswer) :
            base(testID, text, number, score, rightAnswer){ }

    }
}
