using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.TestSettings.Checking
{
    public class ScoreDistributionMethod: Enumeration<ScoreDistributionMethod>
    {
        #region Опции
        public static readonly ScoreDistributionMethod AUTOMATIC = new(1, "Автоматически");

        public static readonly ScoreDistributionMethod MANUAL = new(1, "Вручную");
        #endregion

        public ScoreDistributionMethod(int id, string name) : base(id, name) { }

    }
}
