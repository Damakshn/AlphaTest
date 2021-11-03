using System.Collections.Generic;

namespace AlphaTest.WebApi.Models.Tests
{
    public class EditQuestionRequest
    {
        public string QuestionType { get; set; }

        public int? Score { get; set; }

        public string Text { get; set; }

        public decimal? NumericAnswer { get; set; }

        public string TextualAnswer { get; set; }

        public List<OptionData> Options { get; set; }
    }
}
