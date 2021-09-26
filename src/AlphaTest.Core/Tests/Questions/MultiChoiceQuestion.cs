using AlphaTest.Core.Answers;
using AlphaTest.Core.Checking;
using AlphaTest.Core.Tests.Questions.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaTest.Core.Tests.Questions
{
    public class MultiChoiceQuestion : QuestionWithChoices
    {
        private MultiChoiceQuestion() : base() { }

        internal MultiChoiceQuestion(
            Guid testID,
            QuestionText text,
            uint number,
            QuestionScore score,
            List<(string text, uint number, bool isRight)> optionsData) :
            base(testID, text, number, score, optionsData) { }

        public override MultiChoiceQuestion ReplicateForNewEdition(Test newEdition)
        {
            MultiChoiceQuestion replica = (MultiChoiceQuestion)this.MemberwiseClone();
            replica.ID = Guid.NewGuid();
            replica.TestID = newEdition.ID;
            List<QuestionOption> copiedOptions = new();
            foreach (var option in this.Options)
            {
                copiedOptions.Add(new QuestionOption(this.ID, option.Text, option.Number, option.IsRight));
            }
            replica.Options = copiedOptions;
            return replica;
        }
        
        protected override void CheckSpecificRulesForOptions(List<(string text, uint number, bool isRight)> optionsData)
        {
            CheckRule(new AtLeastOneQuestionOptionMustBeRightRule(optionsData));
        }

        public override PreliminaryResult AcceptCheckingVisitor(CheckingVisitor visitor)
        {
            return visitor.CheckMultiChoiceQuestion(this);
        }
    }
}
