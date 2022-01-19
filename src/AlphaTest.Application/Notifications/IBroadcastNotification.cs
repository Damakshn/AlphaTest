using System.Collections.Generic;

namespace AlphaTest.Application.Notifications
{
    public interface IBroadcastNotification : INotification
    {
        List<string> Audience { get; protected set; }
    }
}
