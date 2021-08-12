using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.TestSettings.Checking
{
    public class ScoreDistributionMethod: Enumeration
    {
        #region Опции
        public static ScoreDistributionMethod AUTOMATIC = new ScoreDistributionMethod(1, "Автоматически");

        public static ScoreDistributionMethod MANUAL = new ScoreDistributionMethod(1, "Вручную");
        #endregion

        public ScoreDistributionMethod(int id, string name) : base(id, name) { }

    }
}
