using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using AlphaTest.WebApi.Models.Tests.AddQuestion;
using AlphaTest.WebApi.Models.Tests;
using System.IO;
using System.Text;

namespace AlphaTest.WebApi.Utils.Converters
{
    public class AddQuestionRequestJsonConverter : JsonConverter<AddQuestionRequest>
    {
        private static readonly string _discriminator = "QuestionType";

        private static readonly Dictionary<string,Type> _allowedQuestionTypes = new() 
        {
            { "SingleChoiceQuestion", typeof(AddSingleChoiceQuestionRequest) },
            { "MultiChoiceQuestion", typeof(AddMultiChoiceQuestionRequest)},
            { "QuestionWithTextualAnswer", typeof(AddQuestionWithTextualAnswerRequest)},
            { "QuestionWithNumericAnswer", typeof(AddQuestionWithNumericAnswerRequest)},
            { "QuestionWithDetailedAnswer", typeof(AddQuestionWithDetailedAnswerRequest)}
        };

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(AddQuestionRequest).IsAssignableFrom(typeToConvert);
        }

        public override AddQuestionRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
            jsonDocument.RootElement.TryGetProperty(_discriminator, out JsonElement questionTypeElement);
            string questionType = (questionTypeElement.ValueKind == JsonValueKind.String) ? questionTypeElement.GetString() : null;
            if (string.IsNullOrWhiteSpace(questionType))
                throw new JsonException("Тип вопроса не задан.");
            if (_allowedQuestionTypes.Keys.Contains(questionType) == false)
                throw new NotSupportedException($"Неизвестный тип вопроса - {questionType}.");

            string rawJson;
            using (MemoryStream stream = new())
            using (Utf8JsonWriter writer = new(stream, new JsonWriterOptions() { Encoder = options.Encoder }))
            {
                jsonDocument.WriteTo(writer);
                writer.Flush();
                rawJson = Encoding.UTF8.GetString(stream.ToArray());
            }

            try
            {
                return (AddQuestionRequest)JsonSerializer.Deserialize(rawJson, _allowedQuestionTypes[questionType], options);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Некорректное содержимое запроса.", e);
            }
        }

        public override void Write(Utf8JsonWriter writer, AddQuestionRequest value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
