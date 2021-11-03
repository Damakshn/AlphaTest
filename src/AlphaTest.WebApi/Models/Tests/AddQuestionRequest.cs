using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaTest.WebApi.Models.Tests
{
    public class AddQuestionRequest
    {
        [Required]
        public string QuestionType { get; set; }
        
        public string Text { get; set; }
        
        public int Score { get; set; }

        public List<(string text, uint number, bool isRight)> OptionsData { get; set; }

        public List<OptionData> Options { get; set; }

        public string TextualAnswer { get; set; }

        public decimal NumericAnswer { get; set; }
    }
}
