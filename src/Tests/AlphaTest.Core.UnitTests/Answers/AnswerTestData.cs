using AlphaTest.Core.Attempts;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.Questions;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace AlphaTest.Core.UnitTests.Answers
{
    public class AnswerTestData
    {
        public AnswerTestData()
        {
            
        }

        public int ID { get; set; }

        public Test Test { get; set; }

        public Attempt Attempt { get; set; }

        public Question Question { get; set; }

        public int RightOptionID { get; set; }

        public List<int> RightOptions { get; set; }

        public string Value { get; set; }

    }
}
