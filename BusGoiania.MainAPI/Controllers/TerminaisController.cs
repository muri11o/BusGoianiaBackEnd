using BusGoiania.MainAPI.Interfaces;
using BusGoiania.MainDomain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BusGoiania.MainAPI.Controllers
{
    [Authorize]
    [Route("v1/api")]

    public class TerminaisController : MainController
    {
        private readonly ILogger<TerminaisController> _logger;
        private readonly IMiddlewareHttpClient _middlewareClient;
        public TerminaisController(ILogger<TerminaisController> logger,
            IMiddlewareHttpClient middlewareClient,
            INotifier notifier) : base(notifier)
        {
            _logger = logger;
            _middlewareClient = middlewareClient;
        }

        [HttpGet("terminal-onibus")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return CustomResponse(await _middlewareClient.ObterTerminasOnibus());
            }
            catch (Exception ex)
            {
                NotifyError($"Falha ao processar a requisição. Detalhes {ex.Message}");
                return CustomResponse();
            }
        }
    }
}
