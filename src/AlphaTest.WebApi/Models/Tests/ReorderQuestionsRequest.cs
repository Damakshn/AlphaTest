using System;
using System.Collections.Generic;

namespace AlphaTest.WebApi.Models.Tests
{
    public class ReorderQuestionsRequest
    {
        public List<QuestionPosition> QuestionOrder { get; set; }

        public class QuestionPosition
        {
            public Guid QuestionID { get; set; }

            public uint Position { get; set; }
        }
    }
}
