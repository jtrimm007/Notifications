using Notifications.Models.Interfaces;

namespace Notifications.ViewModels
{
    public class EmailVM : INotification
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string SendToAddress { get; set; }
    }
}
