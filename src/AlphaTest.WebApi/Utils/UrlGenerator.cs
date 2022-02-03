using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AlphaTest.Application.UtilityServices.API;

namespace AlphaTest.WebApi.Utils
{
    public class UrlGenerator : IUrlGenerator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;

        public UrlGenerator(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }

        public string GetFullUriByName(string urlName, object values)
        {
            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host;
            return _linkGenerator.GetUriByName(urlName, values, scheme, host);
        }
    }
}
