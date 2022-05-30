using BusGoiania.MiddlewareRMTC.Interfaces;
using BusGoiania.MiddlewareRMTC.ManipuladoresHtml;
using Microsoft.AspNetCore.Mvc;

namespace BusGoiania.MiddlewareRMTC.Controllers
{
    [ApiController]
    [Route("v1/rmtc")]
    public class RmtcController : MainController
    {
        private readonly ILogger<RmtcController> _logger;
        private readonly IRmtcHttpClient _rmtcService;
        public RmtcController(ILogger<RmtcController> logger, IRmtcHttpClient rmtcService, INotifier notifier) : base(notifier)
        {
            _logger = logger;
            _rmtcService = rmtcService;
        }

        [HttpGet("ponto-onibus")]
        public async Task<IActionResult> ObterHorariosPontoOnibus([FromQuery] string numeroPontoOnibus)
        {
            try
            {
                var paginaWeb = await _rmtcService.ObterHorariosPontoOnibus(numeroPontoOnibus);
                var result = PontoOnibusHandler.Handle(paginaWeb);
                return CustomResponse(result);
            }
            catch (Exception ex)
            {
                NotifyError($"Falha ao processar a requisição. Detalhes {ex.Message}");
                return CustomResponse();
            }
        }

        [HttpGet("linha-onibus")]
        public async Task<IActionResult> ObterTabelaHorarioLinhaOnibus([FromHeader] string numeroLinhaOnibus)
        {
            var result = await _rmtcService.ObterTabelaHorarioLinhaOnibus(numeroLinhaOnibus);
            return Ok();
        }

        [HttpGet("terminal-onibus")]
        public async Task<IActionResult> ObterTerminaisOnibus()
        {
            try
            {
                var paginaWeb = await _rmtcService.ObterTerminasOnibus();
                var result = TerminaisOnibusHandler.Handle(paginaWeb);
                return CustomResponse(result);
            }
            catch (Exception ex)
            {
                NotifyError($"Falha ao processar a requisição. Detalhes {ex.Message}");
                return CustomResponse();
            }
        }
    }
}
