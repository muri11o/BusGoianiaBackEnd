using BusGoiania.MainAPI.Interfaces;
using BusGoiania.MainDomain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusGoiania.MainAPI.Controllers
{
    [Authorize]
    [Route("v1/api")]
    
    public class HorariosController : MainController
    {
        private readonly ILogger<HorariosController> _logger;
        private readonly IMiddlewareHttpClient _middlewareClient;
        public HorariosController(ILogger<HorariosController> logger,
            IMiddlewareHttpClient middlewareClient,
            INotifier notifier) : base(notifier)
        {
            _logger = logger;
            _middlewareClient = middlewareClient;
        }

        [HttpGet("ponto-onibus")]
        public async Task<IActionResult> Get([FromQuery] string numeroPontoOnibus)
        {
            try
            {
                return CustomResponse(await _middlewareClient.ObterHorariosPontoOnibus(numeroPontoOnibus));
            }
            catch (Exception ex)
            {
                NotifyError($"Falha ao processar a requisição. Detalhes {ex.Message}");
                return CustomResponse();
            }
        }
    }
}
