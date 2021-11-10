using System;
using System.Collections.Generic;

namespace AlphaTest.WebApi.Models.Examination
{
    public class AcceptMultiChoiceAnswerRequest : AcceptAnswerRequest
    {
        public List<Guid> RightOptions { get; set; }
    }
}
