using AlphaTest.WebApi.Utils.Converters;
using System.Text.Json.Serialization;

namespace AlphaTest.WebApi.Models.Examination
{
    [JsonConverter(typeof(AcceptAnswerJsonConverter))]
    public abstract class AcceptAnswerRequest
    {   
    }
}
