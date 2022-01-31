using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AlphaTest.Core.Users;
using AlphaTest.Application.Notifications;
using AlphaTest.Application.DataAccess.EF.QueryExtensions;
using AlphaTest.Application.Notifications.Messages.UserManagement;
using AlphaTest.Infrastructure.Database;

namespace AlphaTest.Infrastructure.BackgroundJobs.UserManagement
{
    class TemporaryPasswordSupervisionService : BackgroundService
    {
        // MAYBE брать из конфигурации
        private static readonly TimeSpan _delay = TimeSpan.FromMinutes(1);
        private readonly IServiceProvider _serviceProvider;
        private readonly ChannelWriter<INotification> _notificationQueue;

        public TemporaryPasswordSupervisionService(IServiceProvider serviceProvider, ChannelWriter<INotification> notificationQueue)
        {
            _serviceProvider = serviceProvider;
            _notificationQueue = notificationQueue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_delay, stoppingToken);
                using (var scope = _serviceProvider.CreateScope())
                {
                    AlphaTestContext db = scope.ServiceProvider.GetRequiredService<AlphaTestContext>();
                    var usersToSuspend = await db.Users
                        .FilterByLockStatus(false)
                        .HasExpiredTemporaryPassword()
                        .ToListAsync(stoppingToken);
                    foreach (var user in usersToSuspend)
                    {
                        try
                        {
                            user.Suspend();
                            await db.SaveChangesAsync(stoppingToken);
                            AccountSuspendedNotification notification =
                                new(user.FirstName, user.LastName, user.MiddleName, user.Email, SuspendingReason.TemporaryPasswordExpired);
                            await _notificationQueue.WriteAsync(notification, stoppingToken);
                        }
                        catch (Exception e)
                        {
                            // ToDo залогировать ошибку
                            Console.WriteLine(e);
                        }
                    }
                }
            }
        }
    }
}
