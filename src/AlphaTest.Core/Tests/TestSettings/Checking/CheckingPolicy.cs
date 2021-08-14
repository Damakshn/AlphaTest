using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.TestSettings.Checking
{
    public class CheckingPolicy: Enumeration<CheckingPolicy>
    {
        #region Опции
        public static readonly CheckingPolicy STANDARD = new(1, "Стандартная");

        public static readonly CheckingPolicy SOFT = new(1, "Мягкая");

        public static readonly CheckingPolicy HARD = new(1, "Жёсткая");
        #endregion

        public CheckingPolicy(int id, string name) : base(id, name) { }
    }
}
