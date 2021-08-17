using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class Question: Entity
    {
        #region Свойства
        public int ID { get; init; }

        public string Text { get; protected set; }

        public uint Number { get; protected set; }

        public uint Score { get; protected set; }
        #endregion

        #region Конструкторы
        protected Question(){}

        protected Question(string text, uint number, uint score)
        {
            CheckRulesForTextAndScore(text, score);
            Text = text;
            Number = number;
            Score = score;
        }
        #endregion

        #region Методы
        internal void ChangeNumber(uint number)
        {
            Number = number;
        }

        protected void CheckRulesForTextAndScore(string text, uint score)
        {
            CheckRule(new QuestionScoreMustBeInRange(score));
            CheckRule(new QuestionTextLengthCannotBeTooLongRule(text));
        }
        #endregion

    }
}
