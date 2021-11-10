using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using AlphaTest.WebApi.Models.Examination;

namespace AlphaTest.WebApi.Utils.Converters
{
    public class AcceptAnswerJsonConverter : JsonConverter<AcceptAnswerRequest>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(AcceptAnswerRequest).IsAssignableFrom(typeToConvert);
        }

        public override AcceptAnswerRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {   
            AcceptAnswerRequest result = null;
            string propertyName;
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            
            reader.Read();
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                propertyName = reader.GetString();
                reader.Read();
                switch (propertyName)
                {
                    case "RightOptionID":
                        #region Один вариант ответа
                        Guid rightOptionID = reader.GetGuid();
                        result = new AcceptSingleChoiceAnswerRequest()
                        {
                            RightOptionID = rightOptionID
                        };
                        #endregion
                        break;
                    case "RightOptions":
                        #region Несколько вариантов ответа
                        if (reader.TokenType != JsonTokenType.StartArray)
                            throw new JsonException();

                        List<JsonTokenType> arrayTokens = new() { JsonTokenType.String, JsonTokenType.EndArray };
                        List<Guid> rightOptions = new();
                        while (true)
                        {
                            reader.Read();
                            if (arrayTokens.Contains(reader.TokenType) == false)
                                throw new JsonException();
                            if (reader.TokenType == JsonTokenType.EndArray)
                                break;
                            rightOptions.Add(reader.GetGuid());
                        }
                        result = new AcceptMultiChoiceAnswerRequest()
                        {
                            RightOptions = rightOptions
                        };
                        #endregion
                        break;
                    case "DetailedAnswer":
                        #region Развёрнутый ответ
                        string detailedAnswer = reader.GetString();
                        result = new AcceptDetailedAnswerRequest()
                        {
                            DetailedAnswer = detailedAnswer
                        };
                        #endregion
                        break;
                    case "NumericAnswer":
                        #region Точный числовой ответ
                        decimal numericAnswer = reader.GetDecimal();
                        result = new AcceptNumericAnswerRequest() 
                        { 
                            NumericAnswer = numericAnswer
                        };
                        #endregion
                        break;
                    case "TextualAnswer":
                        #region Точный ответ текстом
                        string textualAnswer = reader.GetString();
                        result = new AcceptTextualAnswerRequest()
                        {
                            TextualAnswer = textualAnswer
                        };
                        #endregion
                        break;
                }
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.EndObject)
                throw new JsonException();

            if (result is null)
                throw new JsonException("Содержание ответа не представлено.");
            
            return result;
        }

        public override void Write(Utf8JsonWriter writer, AcceptAnswerRequest value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
