using BusGoiania.MainDomain.Interfaces;
using BusGoiania.MainDomain.Models;
using BusGoiania.MainDomain.Models.Validations;

namespace BusGoiania.MainDomain.Services
{
    public class NumeroPontoFavoritoService : BaseService, INumeroPontoFavoritoService
    {
        private readonly INumeroPontoFavoritoRepository _numeroPontoFavoritoRepository;

        public NumeroPontoFavoritoService(INumeroPontoFavoritoRepository numeroPontoFavoritoRepository,
            INotifier notifier) : base(notifier)
        {
            _numeroPontoFavoritoRepository = numeroPontoFavoritoRepository;
        }

        public async Task<IEnumerable<PontoOnibusFavorito>> ObterTodos(Guid usuarioId)
        {
            var pontosOnibusFavotiros = await _numeroPontoFavoritoRepository.ObterTodos();
            pontosOnibusFavotiros = pontosOnibusFavotiros.Where(p => p.UsuarioId == usuarioId).ToList();

            return pontosOnibusFavotiros;
        }

        public async Task Adicionar(PontoOnibusFavorito pontoOnibusFavorito)
        {
            if (!ExecutarValidacao(new PontoOnibusFavoritoValidation(), pontoOnibusFavorito))
                return;

            if (_numeroPontoFavoritoRepository.Buscar(p => p.UsuarioId == pontoOnibusFavorito.UsuarioId 
            && p.NumeroPonto == pontoOnibusFavorito.NumeroPonto).Result.Any())
            {
                Notificar("Este ponto de ônibus já é seu favorito");
                return;
            }

            await _numeroPontoFavoritoRepository.Adicionar(pontoOnibusFavorito);
        }

        public async Task Remover(Guid id)
        {
            await _numeroPontoFavoritoRepository.Remover(id);
        }

        public void Dispose()
        {
            _numeroPontoFavoritoRepository?.Dispose();
        }

        
    }
}
