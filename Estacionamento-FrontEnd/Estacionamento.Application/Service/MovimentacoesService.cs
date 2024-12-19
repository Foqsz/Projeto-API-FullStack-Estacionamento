using Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface;
using Estacionamento_FrontEnd.Estacionamento.Core.Models;
using System.Text.Json;
using Newtonsoft.Json;

namespace Estacionamento_FrontEnd.Estacionamento.Application.Service
{
    public class MovimentacoesService : IMovimentacoesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string apiEndPoint = "/api/Movimentacao";
        private readonly JsonSerializerOptions _serializerOptions;

        public MovimentacoesService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<IEnumerable<MovimentacaoViewModel>> GetEstacionadosAll()
        {
            var client = myHttpClient();

            var response = await client.GetAsync($"{apiEndPoint}/Estacionados");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<MovimentacaoViewModel>>(content);
            }
            else
            {
                return null;
            }
        }

        public Task<MovimentacaoViewModel> RegistrarEntrada()
        {
            throw new NotImplementedException();
        }

        public Task<MovimentacaoViewModel> RegistrarSaida()
        {
            throw new NotImplementedException();
        }

        private HttpClient myHttpClient()
        {
            var client = _httpClientFactory.CreateClient("EstacionamentoApi");
            return client;
        }
    }
}
