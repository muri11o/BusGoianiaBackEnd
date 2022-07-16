using BusGoiania.MainAPI.DTOs;

namespace BusGoiania.MainAPI.Interfaces
{
    public interface IMiddlewareHttpClient
    {
        Task<IEnumerable<HorarioOnibus>> ObterHorariosPontoOnibus(string numeroPontoOnibus);
        Task<IEnumerable<TerminalOnibus>> ObterTerminasOnibus();
    }
}
