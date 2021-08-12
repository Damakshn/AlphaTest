namespace AlphaTest.Core.Common
{
    public interface IBusinessRule
    {
        string Message { get; }

        bool IsBroken { get; }
    }
}
