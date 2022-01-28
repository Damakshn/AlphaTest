namespace AlphaTest.Application.UtilityServices.API
{
    public interface IUrlGenerator
    {
        string GetFullUriByName(string urlName, object values);
    }
}
