using AlphaTest.Core.Common.Abstractions;

namespace AlphaTest.Core.Users
{
    public class SuspendingReason : Enumeration<SuspendingReason>
    {
        #region Конструкторы
        // for EF
        private SuspendingReason() { }

        public SuspendingReason(int id, string name) : base(id, name) { }
        #endregion

        #region Опции
        public static readonly SuspendingReason TemporaryPasswordExpired = new(1, "Истёк срок действия временного пароля");

        public static readonly SuspendingReason Blacklisted = new(2, "Блокировка по решению администрации");
        #endregion


    }
}
