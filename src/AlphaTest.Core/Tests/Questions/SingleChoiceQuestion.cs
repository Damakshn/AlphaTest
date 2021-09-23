using System;
using System.Collections.Generic;
using AlphaTest.Core.Tests.Questions.Rules;


namespace AlphaTest.Core.Tests.Questions
{
    public class SingleChoiceQuestion: QuestionWithChoices
    {
        private SingleChoiceQuestion() : base() { }

        internal SingleChoiceQuestion(Guid testID, QuestionText text, uint number, QuestionScore score, List<QuestionOption> options):
            base(testID, text, number, score, options){ }

        public override SingleChoiceQuestion ReplicateForNewEdition(Test newEdition)
        {
            SingleChoiceQuestion replica = (SingleChoiceQuestion)this.MemberwiseClone();
            replica.ID = Guid.NewGuid();
            replica.TestID = newEdition.ID;
            List<QuestionOption> copiedOptions = new();
            foreach(var option in this.Options)
            {
                copiedOptions.Add(new QuestionOption(option.Text, option.Number, option.IsRight));
            }
            replica.Options = copiedOptions;
            replica.ID = default;
            return replica;
        }

        protected override void CheckSpecificRulesForOptions(List<QuestionOption> options)
        {
            CheckRule(new ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule(options));
        }
    }
}
