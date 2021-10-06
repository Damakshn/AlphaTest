using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Checking;

namespace AlphaTest.Core.Tests.TestSettings.Checking
{
    public class CheckingPolicy: Enumeration<CheckingPolicy>
    {
        public CheckingPolicy(int id, string name) : base(id, name) { }

        // для EF
        private CheckingPolicy() : base() { }

        #region Опции
        public static readonly CheckingPolicy STANDARD = new(1, "Стандартная");

        public static readonly CheckingPolicy SOFT = new(2, "Мягкая");

        public static readonly CheckingPolicy HARD = new(3, "Жёсткая");
        #endregion

        public AdjustedResult AdjustPreliminaryResult(PreliminaryResult preliminaryResult)
        {
            // MAYBE третий параметр в конструкторе PreliminaryResult здесь выглядит лишним
            // жёсткая оценка - за любой ответ, отличный от верного, баллы снимаются
            if (this == HARD && preliminaryResult.CheckResultType != CheckResultType.Credited)
                return new AdjustedResult(preliminaryResult.Answer.ID, preliminaryResult.FullScore.Value * -1, CheckResultType.NotCredited);
            
            // стандартная оценка - частично верный результат трактуется как неверный, баллы не начисляются
            if (this == STANDARD && preliminaryResult.CheckResultType == CheckResultType.PartiallyCredited)
                return new AdjustedResult(preliminaryResult.Answer.ID, 0, CheckResultType.NotCredited);
            
            
            return new AdjustedResult(preliminaryResult);
        }
    }
}
