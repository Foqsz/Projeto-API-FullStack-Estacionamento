using System.Text;
using Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface;
using Estacionamento_FrontEnd.Estacionamento.Core.Models;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

        public async Task<MovimentacaoViewModel> RegistrarEntrada(string placa, string tipoVeiculo)
        {
            var client = myHttpClient();

            // Crie um objeto para enviar no corpo, se necessário
            var payload = new
            {
                Placa = placa,
                TipoVeiculo = tipoVeiculo
            };

            // Serializar o objeto no corpo
            StringContent content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync($"{apiEndPoint}/Entrada/{placa}/{tipoVeiculo}", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<MovimentacaoViewModel>(apiResponse, _serializerOptions);
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(errorResponse);
                    return null;
                }
            }
        }


        public async Task<bool> RegistrarSaida(int id, string placa)
        {
            var client = myHttpClient();

            var response = await client.DeleteAsync($"{apiEndPoint}/Saida/{id}/{placa}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStreamAsync();
                Console.WriteLine(content);

                return true;
            }

            else
            {
                return false;
            }
        }

        private HttpClient myHttpClient()
        {
            var client = _httpClientFactory.CreateClient("EstacionamentoApi");
            return client;
        }
    }
}
