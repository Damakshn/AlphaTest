using AlphaTest.Core.Common;

namespace AlphaTest.Core.Tests.Publishing.Rules
{
    public class RemarkMustBeProvidedWhenProposalIsDeclinedRule : IBusinessRule
    {
        private readonly string _remark;

        public RemarkMustBeProvidedWhenProposalIsDeclinedRule(string remark)
        {
            _remark = remark;
        }

        public string Message => "При отклонении заявки на публикацию нужно указать причину (отзыв).";

        public bool IsBroken => string.IsNullOrWhiteSpace(_remark);
    }
}
