using BusGoiania.MainDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using BusGoiania.MainDomain.Notifications;

namespace BusGoiania.MainAPI.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected MainController(INotifier notifier) => _notifier = notifier;
        protected ActionResult CustomResponse(ModelStateDictionary modelStade)
        {
            if (!modelStade.IsValid)
                NotifyErrorInvalidModel(modelStade);

            return CustomResponse();
        }
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

        protected void NotifyErrorInvalidModel(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(errorMsg);
            }
        }
        protected void NotifyError(string message) => _notifier.Handle(new Notification(message));

    }
}
