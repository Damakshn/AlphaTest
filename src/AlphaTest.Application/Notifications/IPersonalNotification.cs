namespace AlphaTest.Application.Notifications
{
    public interface IPersonalNotification : INotification
    {
        string AddresseeEmail { get; }

        string AddresseeNameAndLastname { get; }
    }
}
