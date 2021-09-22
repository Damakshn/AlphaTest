using System;
using System.Linq;
using System.Collections.Generic;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests.Questions.Rules;



namespace AlphaTest.Core.Tests.Questions
{
    public class SingleChoiceQuestion: QuestionWithChoices
    {
        private SingleChoiceQuestion() : base() { }

        internal SingleChoiceQuestion(int testID, QuestionText text, uint number, QuestionScore score, List<QuestionOption> options):
            base(testID, text, number, score, options){ }

        public override SingleChoiceQuestion ReplicateForNewEdition(Test newEdition)
        {
            SingleChoiceQuestion replica = (SingleChoiceQuestion)this.MemberwiseClone();
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

        public override bool IsRight(Answer answer)
        {
            if (answer is null)
                throw new ArgumentNullException(nameof(answer));
            if (answer is not SingleChoiceAnswer convertedAnswer)
                throw new InvalidOperationException("Тип вопроса и тип ответа не соответствуют.");
            int rightOptionID = Options.Where(o => o.IsRight).Select(o => o.ID).First();
            return convertedAnswer.RightOptionID == rightOptionID;
        }
    }
}
