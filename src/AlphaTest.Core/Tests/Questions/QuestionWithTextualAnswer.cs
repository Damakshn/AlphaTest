using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithTextualAnswer: QuestionWithExactAnswer<string>
    {
        private QuestionWithTextualAnswer() { }

        internal QuestionWithTextualAnswer(int testID, QuestionText text, uint number, QuestionScore score, string rightAnswer) :
            base(testID, text, number, score, rightAnswer)
        {
            CheckRule(new TextualRightAnswerCannotBeNullOrWhitespaceRule(rightAnswer));
        }

        public override void ChangeRightAnswer(string newRightAnswer)
        {
            CheckRule(new TextualRightAnswerCannotBeNullOrWhitespaceRule(newRightAnswer));
            RightAnswer = newRightAnswer;
        }
    }
}
