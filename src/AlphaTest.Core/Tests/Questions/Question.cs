using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Rules;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class Question: Entity
    {
        #region Свойства
        public int ID { get; protected set; }

        public int TestID { get; protected set; }

        public string Text { get; protected set; }

        public uint Number { get; protected set; }

        public QuestionScore Score { get; protected set; }
        #endregion

        #region Конструкторы
        protected Question(){}

        protected Question(int testID, string text, uint number, QuestionScore score)
        {
            CheckCommonRules(text);
            TestID = testID;
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

        protected void CheckCommonRules(string text)
        {
            CheckRule(new QuestionTextLengthMustBeInRangeRule(text));
        }
        #endregion

    }
}
