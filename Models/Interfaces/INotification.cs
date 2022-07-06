namespace Notifications.Models.Interfaces
{
    public interface INotification
    {
        string Message { get; set; }

        // Address is can be used for phone or email depend upon how you INotification is beening used in a sub-class.
        string SendToAddress { get; set; }
    }
}
