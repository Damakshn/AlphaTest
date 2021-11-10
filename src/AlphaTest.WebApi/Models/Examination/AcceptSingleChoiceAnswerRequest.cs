using System;

namespace AlphaTest.WebApi.Models.Examination
{
    public class AcceptSingleChoiceAnswerRequest : AcceptAnswerRequest
    {
        public Guid RightOptionID { get; set; }
    }
}
