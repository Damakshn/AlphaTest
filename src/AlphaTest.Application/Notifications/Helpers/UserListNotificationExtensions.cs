using AlphaTest.Core.Users;
using System.Collections.Generic;
using System.Linq;


namespace AlphaTest.Application.Notifications.Helpers
{
    static class UserListNotificationExtensions
    {
        public static Dictionary<string, string> ToMailingListDictionary(this List<AlphaTestUser> users)
        {
            return users.ToDictionary(u => u.Email, u => $"{u.FirstName} {u.LastName}");
        }
    }
}
