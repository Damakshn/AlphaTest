using AlphaTest.Core.Common.Abstractions;


namespace AlphaTest.Core.Checking
{
    public class CheckResultType: Enumeration<CheckResultType>
    {
        public CheckResultType(int id, string name): base(id, name) { }

        // для EF
        private CheckResultType() : base() { }

        #region Опции
        public static readonly CheckResultType Credited = new(1, "Зачёт");

        public static readonly CheckResultType NotCredited = new(2, "Незачёт");

        public static readonly CheckResultType PartiallyCredited = new(3, "Зачтено частично");
        #endregion
    }
}
