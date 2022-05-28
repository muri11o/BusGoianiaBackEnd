using BusGoiania.MiddlewareRMTC.Configuration;
using BusGoiania.MiddlewareRMTC.Interfaces;
using Microsoft.Extensions.Options;

namespace BusGoiania.MiddlewareRMTC.HttpClients
{
    public class RmtcHttpClient : IRmtcHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public RmtcHttpClient(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }
        public async Task<string> ObterHorariosPontoOnibus(string codBusStop)
        {
            _httpClient.DefaultRequestHeaders.Add("referer", $"{_appSettings.UrlBase}/index.php/pontos-embarque-desembarque?query={codBusStop}");
            var result = await _httpClient.GetStringAsync($"{_appSettings.UrlBase}/index.php?option=com_rmtclinhas&view=pedhorarios&format=raw&ponto={codBusStop}");

            return result;
        }

        public async Task<string> ObterTabelaHorarioLinhaOnibus(string numeroLinhaOnibus)
        {
            var result = await _httpClient.GetStringAsync($"{_appSettings.UrlBase}/components/com_rmtclinhas/frequencia/{numeroLinhaOnibus}.html");
            return result;
        }

        public async Task<string> ObterTerminasOnibus()
        {
            var result = await _httpClient.GetStringAsync($"{_appSettings.UrlBase}/terminais");
            return result;
        }
    }
}
