using Notifications.Models.Interfaces;

namespace Notifications.ViewModels
{
    public class TextMessageVM : INotification
    {
        public string Message { get; set; }
        public string SendToAddress { get; set; }
    }
}
