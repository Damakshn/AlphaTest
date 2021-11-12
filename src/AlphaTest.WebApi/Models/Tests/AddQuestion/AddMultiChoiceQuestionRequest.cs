using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaTest.WebApi.Models.Tests.AddQuestion
{
    public class AddMultiChoiceQuestionRequest : AddQuestionRequest
    {
        public List<OptionData> Options { get; set; }
    }
}
