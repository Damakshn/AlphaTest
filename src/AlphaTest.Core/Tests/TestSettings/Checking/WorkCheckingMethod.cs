using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Tests.TestSettings.Checking
{
    public class WorkCheckingMethod: Enumeration
    {
        #region Опции
        public static WorkCheckingMethod AUTOMATIC = new WorkCheckingMethod(1, "Автоматически");

        public static WorkCheckingMethod MANUAL = new WorkCheckingMethod(1, "Вручную");

        public static WorkCheckingMethod MIXED = new WorkCheckingMethod(1, "Смешанный");
        #endregion

        public WorkCheckingMethod(int id, string name) : base(id, name) { }
    }
}
