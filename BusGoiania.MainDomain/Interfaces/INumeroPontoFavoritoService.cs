using BusGoiania.MainDomain.Models;

namespace BusGoiania.MainDomain.Interfaces
{
    public interface INumeroPontoFavoritoService : IDisposable
    {
        Task<IEnumerable<PontoOnibusFavorito>> ObterTodos(Guid usuarioId);
        Task Adicionar(PontoOnibusFavorito pontoOnibusFavorito);
        Task Remover(Guid id);
    }
}
