using BusGoiania.MainData.Context;
using BusGoiania.MainDomain.Interfaces;
using BusGoiania.MainDomain.Models;

namespace BusGoiania.MainData.Repository
{
    public class NumeroPontoFavoritoRepository : Repository<PontoOnibusFavorito>, INumeroPontoFavoritoRepository
    {
        public NumeroPontoFavoritoRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
