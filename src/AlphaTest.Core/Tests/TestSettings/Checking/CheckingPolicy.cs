using AlphaTest.Core.Common.Abstractions;
using AlphaTest.Core.Checking;

namespace AlphaTest.Core.Tests.TestSettings.Checking
{
    public class CheckingPolicy: Enumeration<CheckingPolicy>
    {
        #region Опции
        public static readonly CheckingPolicy STANDARD = new(1, "Стандартная");

        public static readonly CheckingPolicy SOFT = new(2, "Мягкая");

        public static readonly CheckingPolicy HARD = new(3, "Жёсткая");
        #endregion

        public CheckingPolicy(int id, string name) : base(id, name) { }

        public PreliminaryResult AdjustPreliminaryResult(PreliminaryResult preliminaryResult)
        {
            // MAYBE третий параметр в конструкторе PreliminaryResult здесь выглядит лишним
            // жёсткая оценка - за любой ответ, отличный от верного, баллы снимаются
            if (this == CheckingPolicy.HARD)
            {
                if (preliminaryResult.CheckResultType == CheckResultType.Credited)
                    return new(preliminaryResult.FullScore.Value, CheckResultType.Credited, preliminaryResult.FullScore);
                else
                    return new(-1 * preliminaryResult.FullScore.Value, CheckResultType.NotCredited, preliminaryResult.FullScore);
            }

            // стандартная оценка - частично верный результат трактуется как неверный, баллы не начисляются
            if (this == CheckingPolicy.STANDARD && preliminaryResult.CheckResultType == CheckResultType.PartiallyCredited)
                return new(0, CheckResultType.NotCredited, preliminaryResult.FullScore);
            
            return preliminaryResult;
        }
    }
}
