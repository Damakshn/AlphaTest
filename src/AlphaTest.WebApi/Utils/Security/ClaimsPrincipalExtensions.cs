using System;
using System.Security.Claims;


namespace AlphaTest.WebApi.Utils.Security
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetID(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return Guid.Empty;
            }
            // ToDo иногда id не распознаётся, узнать ЧЗХ
            return Guid.Parse(user.FindFirst("user_id").Value);
        }
    }
}
