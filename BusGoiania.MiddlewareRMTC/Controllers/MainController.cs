using BusGoiania.MiddlewareRMTC.Interfaces;
using BusGoiania.MiddlewareRMTC.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace BusGoiania.MiddlewareRMTC.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected MainController(INotifier notifier) => _notifier = notifier;
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperationIsValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });     
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.GetMessage())
            });
        }
        protected bool OperationIsValid() => !_notifier.ThereIsNotification();
        protected void NotityError(string message) => _notifier.Handle(new Notification(message));
 
    }
}
