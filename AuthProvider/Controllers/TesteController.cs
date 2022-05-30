using BusGoiania.AuthProvider.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusGoiania.AuthProvider.Controllers
{
    [Route("api/teste")]
    [Authorize]
    public class TesteController : MainController
    {
        public TesteController(INotifier notifier) : base(notifier)
        {
        }

        [HttpGet]
        public IActionResult Teste()
        {
            NotifyError("kk");
            return CustomResponse();
        }
    }


}
