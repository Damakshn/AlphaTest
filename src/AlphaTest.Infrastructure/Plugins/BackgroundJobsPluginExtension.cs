using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AlphaTest.Application.Notifications;
using AlphaTest.Infrastructure.BackgroundJobs.Notifications;

namespace AlphaTest.Infrastructure.Plugins
{
    public static class BackgroundJobsPluginExtension
    {
        public static void AddBackgroundJobs(this IServiceCollection services)
        {
            const int maxEmailsInQueue = 2500;
            var notificationChannelOptions = new BoundedChannelOptions(maxEmailsInQueue) { FullMode = BoundedChannelFullMode.Wait };
            services.AddSingleton<Channel<INotification>>(Channel.CreateBounded<INotification>(notificationChannelOptions));
            services.AddSingleton<ChannelWriter<INotification>>(sp =>
            {
                var channel = sp.GetRequiredService<Channel<INotification>>();
                return channel.Writer;
            });
            services.AddSingleton<ChannelReader<INotification>>(sp =>
            {
                var channel = sp.GetRequiredService<Channel<INotification>>();
                return channel.Reader;
            });
            services.AddHostedService<EmailSender>();
        }
    }
}
