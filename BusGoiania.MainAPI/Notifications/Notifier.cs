using BusGoiania.MainAPI.Interfaces;

namespace BusGoiania.MainAPI.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notification) => _notifications.Add(notification);

        public IEnumerable<Notification> GetNotifications() => _notifications;

        public bool ThereIsNotification() => _notifications.Any();
    }
}
