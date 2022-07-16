using BusGoiania.MainAPI.Configuration;
using BusGoiania.MainAPI.DTOs;
using BusGoiania.MainAPI.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace BusGoiania.MainAPI.HttpClients
{
    public class MiddlewareHttpClient : IMiddlewareHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public MiddlewareHttpClient(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }
        public async Task<IEnumerable<HorarioOnibus>> ObterHorariosPontoOnibus(string numeroPontoOnibus)
        {
            var url = $"{_appSettings.UrlMiddleware}/v1/rmtc/ponto-onibus?numeroPontoOnibus={numeroPontoOnibus}";

            var result = await _httpClient.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var horariosOnibus = new List<HorarioOnibus>();

                var json = await result.Content.ReadAsStringAsync();

                foreach (JToken item in JObject.Parse(json)["data"].Children())
                {
                    var itemProperties = item.Children<JProperty>();

                    var numeroLinha = itemProperties.FirstOrDefault(x => x.Name == "numeroLinha");
                    var destino = itemProperties.FirstOrDefault(x => x.Name == "destino");
                    var proximo = itemProperties.FirstOrDefault(x => x.Name == "proximo");
                    var seguinte = itemProperties.FirstOrDefault(x => x.Name == "seguinte");

                    horariosOnibus.Add(new HorarioOnibus 
                    { 
                        NumeroLinha = numeroLinha.Value.ToString(),
                        Destino = destino.Value.ToString(),
                        Proximo = proximo.Value.ToString(),
                        Seguinte = seguinte.Value.ToString()
                    });
                }

                return horariosOnibus;
            }

            throw new Exception($"Status code: {result.StatusCode} - Message: {await result.Content.ReadAsStringAsync()}");
        }

        public async Task<IEnumerable<TerminalOnibus>> ObterTerminasOnibus()
        {
            var url = $"{_appSettings.UrlMiddleware}/v1/rmtc/terminal-onibus";

            var result = await _httpClient.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                var terminais = new List<TerminalOnibus>();

                var json = await result.Content.ReadAsStringAsync();

                foreach (JToken item in JObject.Parse(json)["data"].Children())
                {
                    var itemProperties = item.Children<JProperty>();

                    var terminal = itemProperties.FirstOrDefault(x => x.Name == "terminal");

                    terminais.Add(new TerminalOnibus { Terminal = terminal.Value.ToString() });
                }

                return terminais;
            }

            throw new Exception($"Status code: {result.StatusCode} - Message: {await result.Content.ReadAsStringAsync()}");
        }
    }
}
