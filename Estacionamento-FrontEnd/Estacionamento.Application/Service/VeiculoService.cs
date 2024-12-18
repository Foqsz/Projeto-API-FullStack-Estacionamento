using System.Text;
using Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface;
using Estacionamento_FrontEnd.Estacionamento.Core.Models;
using System.Text.Json;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Estacionamento_FrontEnd.Estacionamento.Application.Service
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IHttpClientFactory _httpClient;
        private const string apiEndPoint = "/api/Veiculos";
        private readonly JsonSerializerOptions _serializerOptions;

        public VeiculoService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<IEnumerable<VeiculosViewModel>> GetVeiculosAll()
        {
            var client = myHttpClient();

            var response = await client.GetAsync($"{apiEndPoint}/ListarVeiculos");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<VeiculosViewModel>>(content);
            }

            throw new HttpRequestException($"Erro ao buscar os veículos. {response.StatusCode}");
        }

        public async Task<VeiculosViewModel> GetVeiculoById(int id)
        {
            var client = myHttpClient();

            var response = await client.GetAsync($"{apiEndPoint}/ChecarVeiculo/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<VeiculosViewModel>(content);
            }

            throw new HttpRequestException($"Erro ao buscar o veículo id {id}. {response.StatusCode}");
        }

        public async Task<VeiculosViewModel> PostVeiculo(VeiculosViewModel veiculo)
        {
            var client = myHttpClient();

            StringContent content = new StringContent(JsonSerializer.Serialize(veiculo), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync($"{apiEndPoint}/CadastrarVeiculo", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<VeiculosViewModel>(apiResponse, _serializerOptions);
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(errorResponse);
                    return null;
                }
            }
        }

        public async Task<VeiculosViewModel> PutVeiculo(int id, VeiculosViewModel veiculo)
        {
            var client = myHttpClient();

            StringContent content = new StringContent(JsonSerializer.Serialize(veiculo), Encoding.UTF8, "application/json");

            using (var response = await client.PutAsync($"{apiEndPoint}/AtualizarVeiculo/{id}", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<VeiculosViewModel>(apiResponse, _serializerOptions);
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(errorResponse);
                    return null;
                }
            }
        }

        public async Task<bool> DeleteVeiculo(int id)
        {
            var client = myHttpClient();

            using (var response = await client.DeleteAsync($"{apiEndPoint}/DeletarVeiculo/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    return true;
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(errorResponse);
                    return false;
                }
            }
        }

        private HttpClient myHttpClient()
        {
            var client = _httpClient.CreateClient("EstacionamentoApi");
            return client;
        }
    }
}
