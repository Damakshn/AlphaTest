using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AlphaTest.Application.Notifications;
using Microsoft.Extensions.Hosting;

namespace AlphaTest.Infrastructure.BackgroundJobs.Notifications
{
    public class EmailSender : IHostedService, IEmailSender
    {
        public Task SendBroadcastNotificationAsync(IBroadcastNotification notification)
        {
            throw new NotImplementedException();
        }

        public Task SendIndividualNotificationAsync(IIndividualNotification notification)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
