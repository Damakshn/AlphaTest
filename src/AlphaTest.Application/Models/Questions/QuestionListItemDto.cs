using System;


namespace AlphaTest.Application.Models.Questions
{
    public class QuestionListItemDto
    {
        public Guid ID { get; set; }

        public string QuestionType { get; set; }

        public string Text { get; set; }

        public int Score { get; set; }

        public uint Number { get; set; }
    }
}
