using BusGoiania.MainDomain.Notifications;

namespace BusGoiania.MainDomain.Interfaces
{
    public interface INotifier
    {
        IEnumerable<Notification> GetNotifications();
        bool ThereIsNotification();
        void Handle(Notification notifcation);
    }
}
