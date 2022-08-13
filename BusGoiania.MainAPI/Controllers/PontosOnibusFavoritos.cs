using AutoMapper;
using BusGoiania.MainAPI.DTOs;
using BusGoiania.MainDomain.Interfaces;
using BusGoiania.MainDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BusGoiania.MainAPI.Controllers
{
    [Authorize]
    [Route("v1/api")]
    public class PontosOnibusFavoritos : MainController
    {
        private readonly ILogger<TerminaisController> _logger;
        private readonly INumeroPontoFavoritoService _numeroPontoFavoritoService;
        private readonly IMapper _mapper;

        public PontosOnibusFavoritos(ILogger<TerminaisController> logger,
            INumeroPontoFavoritoService numeroPontoFavoritoService,
            IMapper mapper,
            INotifier notifier) : base (notifier)
        {
            _logger = logger;
            _numeroPontoFavoritoService = numeroPontoFavoritoService;
            _mapper = mapper;
        }

        [HttpGet("pontos-onibus-favotiros")]
        public async Task<ActionResult<IEnumerable<PontoOnibusFavoritoDTO>>> Get()
        {
            try
            {
                var pontosOnibusFavotirosDTO = _mapper.Map<IEnumerable<PontoOnibusFavoritoDTO>>(await _numeroPontoFavoritoService.ObterTodos(ObterUsuarioId(Request)));

                return CustomResponse(pontosOnibusFavotirosDTO);
              
            }
            catch (Exception ex)
            {
                NotifyError($"Falha ao processar a requisição. Detalhes {ex.Message}");
                return CustomResponse();
            }
        }

        [HttpGet("adicionar-favoritos")]
        public async Task<ActionResult<PontoOnibusFavoritoDTO>> Get([FromQuery] string numeroPonto)
        {
            try
            {
                var pontoOnibusFavorito = new PontoOnibusFavorito();
                pontoOnibusFavorito.UsuarioId = ObterUsuarioId(Request);
                pontoOnibusFavorito.NumeroPonto = numeroPonto;

                await _numeroPontoFavoritoService.Adicionar(pontoOnibusFavorito);

                return CustomResponse(_mapper.Map<PontoOnibusFavoritoDTO>(pontoOnibusFavorito));

            }
            catch (Exception ex)
            {
                NotifyError($"Falha ao processar a requisição. Detalhes {ex.Message}");
                return CustomResponse();
            }
        }

        [HttpDelete("pontos-onibus-favotiros")]
        public async Task<ActionResult<PontoOnibusFavoritoDTO>> Delete([FromQuery] Guid id)
        {
            try
            {
                await _numeroPontoFavoritoService.Remover(id);

                return CustomResponse();

            }
            catch (Exception ex)
            {
                NotifyError($"Falha ao processar a requisição. Detalhes {ex.Message}");
                return CustomResponse();
            }
        }

        private Guid ObterUsuarioId(HttpRequest request)
        {
            var token = request.Headers["Authorization"].ToString();
            token = token.Substring(7, token.Length - 7);

            var usuarioId = new JwtSecurityToken(token).Subject;

            return Guid.Parse(usuarioId);
        }
    }
}
