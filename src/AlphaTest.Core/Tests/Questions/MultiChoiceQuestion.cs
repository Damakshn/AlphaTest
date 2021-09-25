using AlphaTest.Core.Answers;
using AlphaTest.Core.Checking;
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

        public override PreliminaryResult CheckAnswer(Answer answer)
        {
            // MAYBE стоит куда-то вынести, так как похоже на нарушение SRP
            if (answer is null)
                throw new ArgumentNullException(nameof(answer));
            if (answer is not MultiChoiceAnswer convertedAnswer)
                throw new InvalidOperationException("Тип вопроса и тип ответа не соответствуют.");
            
            decimal preliminaryScore = 0;
            CheckResultType preliminaryVerdict = CheckResultType.NotCredited;

            bool allRightOptionsSelected = 
                Options.Where(o => o.IsRight).All(o => convertedAnswer.RightOptions.Contains(o.ID));

            if (allRightOptionsSelected)
            {
                preliminaryScore = Score.Value;
                // если помимо верных ответов выбрано что-то лишнее, то ответ засчитывается как частично верный
                if (convertedAnswer.RightOptions.Count > Options.Where(o => o.IsRight).Count())
                    preliminaryVerdict = CheckResultType.PartiallyCredited;
                else
                    preliminaryVerdict = CheckResultType.Credited;
            }
            else
            {
                foreach (var rightOption in Options.Where(o => o.IsRight))
                {
                    if (convertedAnswer.RightOptions.Contains(rightOption.ID))
                        preliminaryScore += Score.Value / Options.Count;
                }
                preliminaryVerdict = preliminaryScore == 0 ? CheckResultType.NotCredited : CheckResultType.PartiallyCredited;
            }

            return new PreliminaryResult(preliminaryScore, preliminaryVerdict, Score);
        }
    }
}
