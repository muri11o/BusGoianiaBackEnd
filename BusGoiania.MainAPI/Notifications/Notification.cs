namespace BusGoiania.MainAPI.Notifications
{
    public class Notification
    {
        private string _message;

        public Notification(string message)
        {
            _message = message;
        }

        public string GetMessage() => _message;
    }
}
