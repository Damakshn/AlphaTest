using System;
using AlphaTest.Core.Tests;
using AlphaTest.Core.Tests.TestSettings.Checking;
using AlphaTest.Core.Tests.TestSettings.TestFlow;

namespace AlphaTest.Application.Models.Tests
{
    // ToDo QuestionCount
    // ToDo list of contributors
    public class TestInfo
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public string Topic { get; set; }

        public int Version { get; set; }

        public ContributorInfo AuthorInfo { get; set; }

        public TestStatus Status { get; set; }

        public RevokePolicy RevokePolicy { get; set; }

        public TimeSpan? TimeLimit { get; set; }

        public uint? AttemptsLimit { get; set; }

        public NavigationMode NavigationMode { get; set; }
                
        public CheckingPolicy CheckingPolicy { get; set; }

        public WorkCheckingMethod WorkCheckingMethod { get; set; }

        public uint PassingScore { get; set; }

        public ScoreDistributionMethod ScoreDistributionMethod { get; set; }

        public QuestionScore ScorePerQuestion { get; set; }
    }
}
