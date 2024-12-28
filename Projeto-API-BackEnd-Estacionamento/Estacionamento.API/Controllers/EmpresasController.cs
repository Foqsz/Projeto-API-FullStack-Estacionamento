using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmpresasController : ControllerBase
{
    private readonly IEmpresasService _empresasService;
    private readonly HybridCache _hybridCache;
    private readonly string cacheKey = "empresas";
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public EmpresasController(IEmpresasService empresasService, ILogger<EmpresasController> logger, IMapper mapper, HybridCache hybridCache)
    {
        _empresasService = empresasService;
        _logger = logger;
        _mapper = mapper;
        _hybridCache = hybridCache;
    }

    #region Listar Todas as Empresas

    [HttpGet("ListarEmpresas")]
    //[Authorize]
    [SwaggerOperation(Summary = "Lista todas as empresas.", Description = "Retorna todas as empresas do banco de dados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IEnumerable<EmpresaDTO>> GetEmpresasAll()
    {
        return await _hybridCache.GetOrCreateAsync(cacheKey, async cancellationToken =>
            {
                await Task.Delay(3000);
                var empresas = await _empresasService.GetAllEmpresasService();
                return empresas;
            },
            new HybridCacheEntryOptions
            {
                //tempo expiração cache distribuido
                Expiration = TimeSpan.FromSeconds(20),
                //tempo expiração cache memoria
                LocalCacheExpiration = TimeSpan.FromSeconds(25),
            },
            ["empresas-tag"]
        );
    }

    #endregion

    #region Checar uma empresa pelo ID

    [HttpGet("ChecarEmpresa/{id}")]
    [SwaggerOperation(Summary = "Checa uma empresa de acordo com o ID informado",
                      Description = "Retorna as informações da empresa com o id informado.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmpresaDTO>> GetEmpresaId(int id)
    {
        string cacheKey = $"empresa-{id}";

        return await _hybridCache.GetOrCreateAsync(cacheKey, async cancellationToken =>
            {
                await Task.Delay(3000);
                var empresa = await _empresasService.GetEmpresaIdService(id);
                return empresa;
            },
            new HybridCacheEntryOptions
            {
                //tempo expiração cache distribuido
                Expiration = TimeSpan.FromSeconds(20),
                //tempo expiração cache memoria
                LocalCacheExpiration = TimeSpan.FromSeconds(25),
            },
            ["empresa-tag"]
        );
    }

    #endregion

    #region Criar uma empresa

    [HttpPost("CriarEmpresa")]
    [SwaggerOperation(Summary = "Adiciona uma empresa ao sistema.", Description = "Retorna a criação de uma empresa.")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmpresaDTO>> PostEmpresa(EmpresaDTO empresa)
    {
        var createEmpresa = await _empresasService.CreateEmpresaService(empresa);

        if (createEmpresa == null)
        {
            _logger.LogError("Não foi possível criar a empresa informada.");
            return NotFound();
        }

        await _hybridCache.RemoveAsync(cacheKey);
        _logger.LogInformation("Empresa criada com sucesso.");
        return CreatedAtAction(nameof(GetEmpresaId), new { id = createEmpresa.Id }, createEmpresa);
    }

    #endregion

    #region Editar uma empresa

    [HttpPut("EditarEmpresa/{id}")]
    [SwaggerOperation(Summary = "Atualiza as informações de uma empresa", Description = "Edita as informações da empresa.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmpresaDTO>> PutEmpresa(int id, EmpresaDTO empresa)
    {
        var empresaExistente = await _empresasService.GetEmpresaIdService(id);

        if (empresaExistente == null)
        {
            return NotFound("Não foi possível atualizar a empresa informada.");
        }

        if (id != empresa.Id)
        {
            _logger.LogError("O ID da empresa não corresponde ao ID fornecido.");
            return BadRequest("O ID da empresa não corresponde ao ID fornecido.");
        }

        var updateEmpresa = await _empresasService.UpdateEmpresaService(id, empresa);

        var empresaAtualizada = _mapper.Map<EmpresaDTO>(updateEmpresa);

        await _hybridCache.RemoveAsync(cacheKey);
        return Ok(empresaAtualizada);
    }

    #endregion

    #region Deletar uma Empresa

    [HttpDelete("DeletarEmpresa/{id}")]
    [SwaggerOperation(Summary = "Remove uma empresa do sistema.", Description = "Deleta uma empresa desejada.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteEmpresa(int id)
    {
        var deleteEmpresa = await _empresasService.DeleteEmpresaService(id);

        await _hybridCache.RemoveAsync(cacheKey);
        return Ok(deleteEmpresa);
    }

    #endregion
}
