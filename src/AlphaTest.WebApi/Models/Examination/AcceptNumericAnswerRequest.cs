namespace AlphaTest.WebApi.Models.Examination
{
    public class AcceptNumericAnswerRequest : AcceptAnswerRequest
    {
        public decimal NumericAnswer { get; set; }
    }
}
