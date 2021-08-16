using System.Collections.Generic;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class QuestionWithChoices: Question
    {
        public List<QuestionOption> Options { get; private set; }
    }
}
