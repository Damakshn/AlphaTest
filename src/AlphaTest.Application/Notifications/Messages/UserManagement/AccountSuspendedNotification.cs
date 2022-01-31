using System.Text;
using AlphaTest.Core.Users;

namespace AlphaTest.Application.Notifications.Messages.UserManagement
{
    public class AccountSuspendedNotification : IPersonalNotification
    {
        private readonly SuspendingReason _reason;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _middleName;

        public AccountSuspendedNotification(string firstName, string lastName, string middleName, string addresseeEmail, SuspendingReason reason)
        {
            AddresseeEmail = addresseeEmail;
            _firstName = firstName;
            _lastName = lastName;
            _middleName = middleName;
            _reason = reason;
        }

        public string AddresseeEmail { get; protected set; }

        public string AddresseeNameAndLastname => $"{_firstName} {_lastName}";

        public string Message {
            get {
                StringBuilder builder = new();
                builder.Append($"{_firstName} {_middleName}, ваша учётная запись в системе AlphaTest была заблокирована\n");
                // ToDo добавить инструкцию для заблокированного пользователя
                builder.Append($"Причина - {_reason.Name}\n");
                builder.Append(MailSignature.AlphaTestSignature);
                return builder.ToString(); 
            }
        }

        public string Subject => "AlphaTest - учётная запись заблокирована";
    }
}
