using BusGoiania.AuthProvider.Notifications;

namespace BusGoiania.AuthProvider.Interfaces
{
    public interface INotifier
    {
        IEnumerable<Notification> GetNotifications();
        bool ThereIsNotification();
        void Handle(Notification notifcation);
    }
}
