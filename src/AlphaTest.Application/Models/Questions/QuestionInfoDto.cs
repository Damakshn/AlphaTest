using System;
using System.Collections.Generic;

namespace AlphaTest.Application.Models.Questions
{
    public class QuestionInfoDto
    {
        public Guid ID { get; set; }

        public Guid TestID { get; set; }

        public string QuestionType { get; set; }

        public string Text { get; set; }

        public int Score { get; set; }

        // MAYBE сделать 2 поля (для числа и для текста)
        public string RightAnswer { get; set; }

        public List<QuestionOptionDto> Options { get; set; }

        public uint Number { get; set; }
    }
}
