using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests.Questions.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlphaTest.Core.Tests.Questions
{
    public class MultiChoiceQuestion: QuestionWithChoices
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

        public override bool IsRight(Answer answer)
        {
            if (answer is null)
                throw new ArgumentNullException(nameof(answer));
            if (answer is not MultiChoiceAnswer convertedAnswer)
                throw new InvalidOperationException("Тип вопроса и тип ответа не соответствуют.");
            return Options.All(o =>
                o.IsRight
                ? convertedAnswer.RightOptions.Contains(o.ID)
                : !convertedAnswer.RightOptions.Contains(o.ID)
            );
        }
    }
}
