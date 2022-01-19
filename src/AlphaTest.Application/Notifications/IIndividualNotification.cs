namespace AlphaTest.Application.Notifications
{
    public interface IIndividualNotification : INotification
    {
        string Addressee { get; protected set; }
    }
}
