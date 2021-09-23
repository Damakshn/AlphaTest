using System;
using AlphaTest.Core.Tests.Questions.Rules;
using System.Collections.Generic;

namespace AlphaTest.Core.Tests.Questions
{
    public class MultiChoiceQuestion: QuestionWithChoices
    {
        private MultiChoiceQuestion() : base() { }

        internal MultiChoiceQuestion(int testID, QuestionText text, uint number, QuestionScore score, List<QuestionOption> options) :
            base(testID, text, number, score, options){ }

        public override MultiChoiceQuestion ReplicateForNewEdition(Test newEdition)
        {
            MultiChoiceQuestion replica = (MultiChoiceQuestion)this.MemberwiseClone();
            replica.ID = Guid.NewGuid();
            replica.TestID = newEdition.ID;
            List<QuestionOption> copiedOptions = new();
            foreach (var option in this.Options)
            {
                copiedOptions.Add(new QuestionOption(option.Text, option.Number, option.IsRight));
            }
            replica.Options = copiedOptions;
            replica.ID = default;
            return replica;
        }

        protected override void CheckSpecificRulesForOptions(List<QuestionOption> options)
        {
            CheckRule(new AtLeastOneQuestionOptionMustBeRightRule(options));
        }
    }
}
