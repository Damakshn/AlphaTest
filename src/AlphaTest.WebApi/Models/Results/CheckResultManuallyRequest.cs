using System;

namespace AlphaTest.WebApi.Models.Results
{
    public class CheckResultManuallyRequest
    {
        public Guid AnswerID { get; set; }

        public decimal Score { get; set; }

        public int CheckResultTypeID { get; set; }
    }
}
