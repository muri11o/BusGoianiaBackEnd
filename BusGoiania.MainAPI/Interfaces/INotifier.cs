using BusGoiania.MainAPI.Notifications;

namespace BusGoiania.MainAPI.Interfaces
{
    public interface INotifier
    {
        IEnumerable<Notification> GetNotifications();
        bool ThereIsNotification();
        void Handle(Notification notifcation);
    }
}
