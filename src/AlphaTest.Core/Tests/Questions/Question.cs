using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Questions.Rules;

namespace AlphaTest.Core.Tests.Questions
{
    public abstract class Question: Entity
    {
        #region Свойства
        public int ID { get; protected set; }

        public int TestID { get; protected set; }

        public QuestionText Text { get; protected set; }

        public uint Number { get; protected set; }

        public QuestionScore Score { get; protected set; }
        #endregion

        #region Конструкторы
        protected Question(){}

        protected Question(int testID, QuestionText text, uint number, QuestionScore score)
        {   
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
        #endregion

    }
}
