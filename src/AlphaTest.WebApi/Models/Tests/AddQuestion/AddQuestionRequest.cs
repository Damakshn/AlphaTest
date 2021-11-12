using AlphaTest.WebApi.Utils.Converters;
using System.Text.Json.Serialization;

namespace AlphaTest.WebApi.Models.Tests.AddQuestion
{
    [JsonConverter(typeof(AddQuestionRequestJsonConverter))]
    public abstract class AddQuestionRequest
    {
        public string QuestionType { get; set; }

        public string Text { get; set; }

        public int Score { get; set; }
    }
}
