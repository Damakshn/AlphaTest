using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.TestSettings.TestFlow
{
    public class NavigationMode: Enumeration
    {
        #region Опции
        public static NavigationMode SEQUENTIONAL = new NavigationMode(1, "Последовательный");

        public static NavigationMode FREE = new NavigationMode(1, "Произвольный");
        #endregion

        public NavigationMode(int id, string name) : base(id, name) { }

    }
}
