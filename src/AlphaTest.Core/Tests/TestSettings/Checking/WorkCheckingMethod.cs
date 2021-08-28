using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.TestSettings.Checking
{
    public class WorkCheckingMethod: Enumeration<WorkCheckingMethod>
    {
        #region Опции
        public static readonly WorkCheckingMethod AUTOMATIC = new(1, "Автоматически");

        public static readonly WorkCheckingMethod MANUAL = new(2, "Вручную");

        public static readonly WorkCheckingMethod MIXED = new(3, "Смешанный");
        #endregion

        public WorkCheckingMethod(int id, string name) : base(id, name) { }
    }
}
