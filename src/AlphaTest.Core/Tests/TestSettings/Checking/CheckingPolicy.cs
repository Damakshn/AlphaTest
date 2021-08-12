using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.TestSettings.Checking
{
    public class CheckingPolicy: Enumeration
    {
        #region Опции
        public static CheckingPolicy STANDARD = new CheckingPolicy(1, "Стандартная");

        public static CheckingPolicy SOFT = new CheckingPolicy(1, "Мягкая");

        public static CheckingPolicy HARD = new CheckingPolicy(1, "Жёсткая");
        #endregion

        public CheckingPolicy(int id, string name) : base(id, name) { }
    }
}
