using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MailKit.Net.Smtp;
using AlphaTest.Application.Notifications;
using MimeKit;

namespace AlphaTest.Infrastructure.BackgroundJobs.Notifications
{
    public class EmailSender : BackgroundService
    {
        private readonly ChannelReader<INotification> _notificationQueue;
        private readonly MailboxAddress _fromAddress;
        private readonly string _smtpServer;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly int _smtpPort;



        public EmailSender(ChannelReader<INotification> notificationQueue, IConfiguration configuration)
        {
            _notificationQueue = notificationQueue;
            _fromAddress = new MailboxAddress(configuration["ALPHATEST:NOTIFICATION_NICKNAME"], configuration["ALPHATEST:NOTIFICATION_EMAIL"]);
            _smtpServer = configuration["ALPHATEST:SMTP_SERVER"];
            _smtpUsername = configuration["ALPHATEST:SMTP_USERNAME"];
            _smtpPassword = configuration["ALPHATEST:SMTP_PASSWORD"];
            _smtpPort = int.Parse(configuration["ALPHATEST:SMTP_PORT"]);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var notification = await _notificationQueue.ReadAsync(stoppingToken);
                MimeMessage message = BuildMessage(notification);
                message.From.Add(_fromAddress);
                await SendMessageAsync(message, stoppingToken);
                Console.WriteLine(message.To);
                Console.WriteLine(message.Subject);
                Console.WriteLine(notification.Message);
            }
        }

        private MimeMessage BuildMessage(INotification notification)
        {
            MimeMessage message = new();
            if (notification is IBroadcastNotification broadcastNotification)
            {
                foreach (var addressee in broadcastNotification.AudienceNew)
                {
                    MailboxAddress address = new(addressee.Value, addressee.Key);
                    message.To.Add(address);
                }
            }
            else if (notification is IPersonalNotification personalNotification)
            {
                message.To.Add(new MailboxAddress(personalNotification.AddresseeNameAndLastname, personalNotification.AddresseeEmail));
            }
            message.Subject = notification.Subject;
            message.Body = new TextPart("plain") { Text = notification.Message };
            return message;
        }

        private async Task SendMessageAsync(MimeMessage message, CancellationToken cancellationToken = default)
        {
            // ToDo check auth, log errors
            // MAYBE много await
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, true, cancellationToken);
                await client.AuthenticateAsync(_smtpUsername, _smtpPassword, cancellationToken);
                await client.SendAsync(message, cancellationToken);
                await client.DisconnectAsync(true, cancellationToken);
            }
        }
    }
}
