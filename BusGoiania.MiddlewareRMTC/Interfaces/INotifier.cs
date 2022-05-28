using BusGoiania.MiddlewareRMTC.Notifications;

namespace BusGoiania.MiddlewareRMTC.Interfaces
{
    public interface INotifier
    {
        IEnumerable<Notification> GetNotifications();
        bool ThereIsNotification();
        void Handle(Notification notifcation);
    }
}
