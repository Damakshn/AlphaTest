using System.Collections.Generic;

namespace AlphaTest.Application.Notifications
{
    public interface IBroadcastNotification : INotification
    {
        Dictionary<string, string> AudienceNew { get; }
    }
}
