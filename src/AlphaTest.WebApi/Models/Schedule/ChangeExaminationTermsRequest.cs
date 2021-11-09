using System;

namespace AlphaTest.WebApi.Models.Schedule
{
    public class ChangeExaminationTermsRequest
    {
        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }
    }
}
