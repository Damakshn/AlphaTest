using System.Collections.Generic;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class QuestionWithChoices: Question
    {   
        public List<QuestionOption> Options { get; protected set; }
        
        protected QuestionWithChoices(): base() { }

        internal QuestionWithChoices(string text, uint number, uint score) : base(text, number, score) { }

        internal abstract void ChangeAttributes(string text, uint score, List<QuestionOption> options);

    }
}
