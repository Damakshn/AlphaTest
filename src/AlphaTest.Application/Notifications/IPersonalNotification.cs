namespace AlphaTest.Application.Notifications
{
    public interface IPersonalNotification : INotification
    {
        string Addressee { get; }
    }
}
