using System;
using System.Linq;
using System.Collections.Generic;
using AlphaTest.Core.Answers;
using AlphaTest.Core.Tests.Questions.Rules;
using AlphaTest.Core.Checking;

namespace AlphaTest.Core.Tests.Questions
{
    public class SingleChoiceQuestion: QuestionWithChoices
    {
        private SingleChoiceQuestion() : base() { }

        internal SingleChoiceQuestion(
            Guid testID,
            QuestionText text,
            uint number,
            QuestionScore score,
            List<(string text, uint number, bool isRight)> optionsData) :
            base(testID, text, number, score, optionsData)
        { }

        public override SingleChoiceQuestion ReplicateForNewEdition(Test newEdition)
        {
            SingleChoiceQuestion replica = (SingleChoiceQuestion)this.MemberwiseClone();
            replica.ID = Guid.NewGuid();
            replica.TestID = newEdition.ID;
            List<QuestionOption> copiedOptions = new();
            foreach(var option in this.Options)
            {
                copiedOptions.Add(new QuestionOption(this.ID, option.Text, option.Number, option.IsRight));
            }
            replica.Options = copiedOptions;
            return replica;
        }

        protected override void CheckSpecificRulesForOptions(List<(string text, uint number, bool isRight)> optionsData)
        {
            CheckRule(new ForSingleChoiceQuestionMustBeExactlyOneRightOptionRule(optionsData));
        }

        public override bool IsRight(Answer answer)
        {
            if (answer is null)
                throw new ArgumentNullException(nameof(answer));
            if (answer is not SingleChoiceAnswer convertedAnswer)
                throw new InvalidOperationException("Тип вопроса и тип ответа не соответствуют.");
            Guid rightOptionID = Options.Where(o => o.IsRight).Select(o => o.ID).First();
            return convertedAnswer.RightOptionID == rightOptionID;
        }

        public override PreliminaryResult CheckAnswer(Answer answer)
        {
            // MAYBE стоит куда-то вынести, так как похоже на нарушение SRP
            if (answer is null)
                throw new ArgumentNullException(nameof(answer));
            if (answer is not SingleChoiceAnswer convertedAnswer)
                throw new InvalidOperationException("Тип вопроса и тип ответа не соответствуют.");
            bool isRight = Options.Where(o => o.IsRight).Select(o => o.ID).First() == convertedAnswer.RightOptionID;

            if (isRight)
                return new PreliminaryResult(Score.Value, CheckResultType.Credited, Score);
            else
                return new PreliminaryResult(0, CheckResultType.NotCredited, Score);
        }
    }
}
