using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusGoiania.MainAPI.API.Controllers
{
    [ApiController]
    [Route("api/teste")]
    [Authorize]
    public class TesteController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok("I love youu");
        }
    }
}
