using AlphaTest.Core.Common.Abstractions;
using System;

namespace AlphaTest.Core.Answers
{
    public class ChosenOption : Entity
    {
        public Guid AnswerID { get; private set; }

        public Guid OptionID { get; private set; }

        // для EF
        private ChosenOption() { }

        internal ChosenOption(Guid answerID, Guid optionID)
        {
            AnswerID = answerID;
            OptionID = optionID;
        }
    }
}
