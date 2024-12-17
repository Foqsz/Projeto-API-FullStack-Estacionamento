using System.Text;
using System.Text.Json; 
using Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface;
using Estacionamento_FrontEnd.Estacionamento.Core.Models;
using Newtonsoft.Json; 
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Estacionamento_FrontEnd.Estacionamento.Application.Service;

public class EmpresaService : IEmpresaService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string apiEndPoint = "/api/Empresas";
    private readonly JsonSerializerOptions _serializerOptions; 

    public EmpresaService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<EmpresaViewModel>> GetEmpresaAll()
    {
        var client = myHttpClient();

        var response = await client.GetAsync($"{apiEndPoint}/ListarEmpresas");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<EmpresaViewModel>>(content);
        }

        throw new HttpRequestException($"Erro ao buscar as empresa. {response.StatusCode}");
    }

    public async Task<EmpresaViewModel> GetEmpresaById(int id)
    {
        var client = myHttpClient();

        var response = await client.GetAsync($"{apiEndPoint}/ChecarEmpresa/{id}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<EmpresaViewModel>(content);
        }
        throw new HttpRequestException($"Erro ao buscar a empresa id {id}. {response.StatusCode}");
    }

    public async Task<EmpresaViewModel> PostEmpresa(EmpresaViewModel empresa)
    {
        var client = myHttpClient();

        StringContent content = new StringContent(JsonSerializer.Serialize(empresa), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync($"{apiEndPoint}/CriarEmpresa", content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<EmpresaViewModel>(apiResponse, _serializerOptions);
            }
            else
            {
                return null;
            }
        }
    }

    public Task<EmpresaViewModel> PutEmpresa(int id, EmpresaViewModel empresa)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeletarEmpresa(int id)
    {
        var client = myHttpClient();

        using (var response = await client.DeleteAsync($"{apiEndPoint}/DeletarEmpresa/{id}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private HttpClient myHttpClient()
    {
        var client = _httpClientFactory.CreateClient("EstacionamentoApi");
        return client;
    }
}

