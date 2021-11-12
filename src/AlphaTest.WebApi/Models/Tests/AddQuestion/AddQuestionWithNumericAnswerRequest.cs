namespace AlphaTest.WebApi.Models.Tests.AddQuestion
{
    public class AddQuestionWithNumericAnswerRequest : AddQuestionRequest
    {
        public decimal RightAnswer { get; set; }
    }
}
