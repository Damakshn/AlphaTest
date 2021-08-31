using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.TestSettings.Checking
{
    public class ScoreDistributionMethod: Enumeration<ScoreDistributionMethod>
    {
        #region Опции
        public static readonly ScoreDistributionMethod UNIFIED = new(1, "Автоматически");

        public static readonly ScoreDistributionMethod MANUAL = new(2, "Вручную");
        #endregion

        public ScoreDistributionMethod(int id, string name) : base(id, name) { }

    }
}
