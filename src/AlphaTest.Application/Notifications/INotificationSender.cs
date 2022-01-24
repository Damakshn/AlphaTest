using System.Threading.Tasks;

namespace AlphaTest.Application.Notifications
{
    public interface INotificationSender
    {
        Task SendIndividualNotificationAsync(IPersonalNotification notification);

        Task SendBroadcastNotificationAsync(IBroadcastNotification notification);
    }
}
