namespace AlphaTest.Application.Notifications
{
    public interface INotification
    {
        string Message { get; }

        string Subject { get; }
    }
}
