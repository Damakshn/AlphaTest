using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;
using Microsoft.Extensions.Hosting;
using AlphaTest.Application.Notifications;


namespace AlphaTest.Infrastructure.BackgroundJobs.Notifications
{
    public class EmailSender : BackgroundService
    {
        private readonly ChannelReader<INotification> _notificationQueue;

        public EmailSender(ChannelReader<INotification> notificationQueue)
        {
            _notificationQueue = notificationQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var notification = await _notificationQueue.ReadAsync(stoppingToken);
                Console.WriteLine(notification.Message);
            }
        }
    }
}
