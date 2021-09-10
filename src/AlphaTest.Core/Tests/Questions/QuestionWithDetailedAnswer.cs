namespace AlphaTest.Core.Tests.Questions
{
    public class QuestionWithDetailedAnswer: Question
    {
        private QuestionWithDetailedAnswer():base() { }

        internal QuestionWithDetailedAnswer(int testID, QuestionText text, uint number, QuestionScore score)
            : base(testID, text, number, score) { }

        public override QuestionWithDetailedAnswer ReplicateForNewEdition(Test newEdition)
        {
            QuestionWithDetailedAnswer replica = (QuestionWithDetailedAnswer)this.MemberwiseClone();
            replica.TestID = newEdition.ID;
            replica.ID = default;
            return replica;
        }

    }
}
