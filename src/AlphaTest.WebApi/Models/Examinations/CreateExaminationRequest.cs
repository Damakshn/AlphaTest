using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaTest.WebApi.Models.Examinations
{
    public class CreateExaminationRequest
    {
        public Guid TestID { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }

        public List<Guid> Groups { get; set; }

    }
}
