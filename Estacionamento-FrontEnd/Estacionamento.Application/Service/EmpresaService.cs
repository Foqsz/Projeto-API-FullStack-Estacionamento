using System.Text.Json;
using Estacionamento_FrontEnd.Estacionamento.Core.Models;

namespace Estacionamento_FrontEnd.Estacionamento.Application.Service;

public class EmpresaService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string apiEndPoint = "/api/Empresas";
    private readonly JsonSerializerOptions _serializerOptions;
    private EmpresaViewModel _empresaViewModel;

    public EmpresaService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }


}

