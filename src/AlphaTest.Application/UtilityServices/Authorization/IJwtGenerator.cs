using AlphaTest.Core.Users;
using System.Threading.Tasks;

namespace AlphaTest.Application.UtilityServices.Authorization
{
    public interface IJwtGenerator
    {
        Task<string> GetTokenAsync(AlphaTestUser user);
    }
}
