using System.Threading.Tasks;

namespace AlphaTest.Application.Notifications
{
    public interface INotificationSender
    {
        Task SendIndividualNotificationAsync(IIndividualNotification notification);

        Task SendBroadcastNotificationAsync(IBroadcastNotification notification);
    }
}
