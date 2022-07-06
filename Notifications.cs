using Notifications.Models.Interfaces;
using System.Threading.Tasks;

namespace Notifications
{
    public abstract class Notifications
    {
        public string Message { get; set; }
        public string SendToAddress { get { return _address; } }
        private string _address;

        public Notifications(INotification notification)
        {
            Message = notification.Message;
            _address = notification.SendToAddress;

        }
        public abstract Task SendNotification();

        public abstract void ConstructMessageToSend();
    }
}
