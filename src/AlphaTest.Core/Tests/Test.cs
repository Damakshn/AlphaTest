using System.Collections.Generic;
using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Tests.Questions;
using AlphaTest.Core.Tests.TestSettings.TestFlow;
using AlphaTest.Core.Tests.TestSettings.Checking;

namespace AlphaTest.Core.Tests
{
    public class Test: Entity
    {
        #region Основные атрибуты
        public int ID { get; init; }

        public string Title { get; private set; }

        public string Topic { get; private set; }

        public int Version { get; private set; }

        public int AuthorID { get; private set; }

        public List<Question> Questions { get; private set; }

        #endregion

        #region Настройки процедуры тестирования
        public RevokePolicy RevokePolicy { get; private set; } = new RevokePolicy(false);

        public TimeLimit TimeLimit { get; private set; } = null;

        public uint RetriesLimit { get; private set; } = 1;

        public NavigationMode NavigationMode { get; private set; } = NavigationMode.SEQUENTIONAL;
        #endregion

        #region Настройки оценивания
        public CheckingPolicy CheckingPolicy { get; private set; } = CheckingPolicy.STANDARD;

        public WorkCheckingMethod WorkCheckingMethod { get; private set; } = WorkCheckingMethod.MANUAL;

        public uint PassingScore { get; private set; } = 0;

        public ScoreDistributionMethod ScoreDistributionMethod { get; private set; } = ScoreDistributionMethod.AUTOMATIC;

        public uint ScorePerQuestion { get; private set; } = 1
        #endregion

        public Test() {}
    }
}
