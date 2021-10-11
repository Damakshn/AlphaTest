using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.TestSettings.TestFlow
{
    public class NavigationMode: Enumeration<NavigationMode>
    {
        public NavigationMode(int id, string name) : base(id, name) { }

        // для EF
        private NavigationMode() : base() { }

        #region Опции
        public static readonly NavigationMode SEQUENTIONAL = new(1, "Последовательный");

        public static readonly NavigationMode FREE = new(2, "Произвольный");
        #endregion
    }
}
