namespace BusGoiania.MiddlewareRMTC.Interfaces
{
    public interface IRmtcHttpClient
    {
        Task<string> ObterHorariosPontoOnibus(string numeroPontoOnibus);
        Task<string> ObterTabelaHorarioLinhaOnibus(string numeroLinhaOnibus);
        Task<string> ObterTerminasOnibus();
    }
}
