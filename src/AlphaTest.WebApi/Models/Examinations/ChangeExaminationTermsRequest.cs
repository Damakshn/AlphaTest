using System;

namespace AlphaTest.WebApi.Models.Examinations
{
    public class ChangeExaminationTermsRequest
    {
        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }
    }
}
