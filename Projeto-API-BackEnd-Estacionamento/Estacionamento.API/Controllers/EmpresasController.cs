using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmpresasController : ControllerBase
{
    private readonly IEmpresasService _empresasService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public EmpresasController(IEmpresasService empresasService, ILogger<EmpresasController> logger, IMapper mapper)
    {
        _empresasService = empresasService;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("ListarEmpresas")]
    [Authorize]
    [SwaggerOperation(Summary = "Lista todas as empresas.", Description = "Retorna todas as empresas do banco de dados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<EmpresaDTO>>> GetEmpresasAll()
    {
        var empresasAll = await _empresasService.GetAllEmpresasService();

        if (empresasAll == null)
        {
            _logger.LogError("Controller: Nenhuma empresa localizada.");
            return NotFound();
        }

        var empresaMapper = _mapper.Map<IEnumerable<EmpresaDTO>>(empresasAll);

        return Ok(empresaMapper);
    }

    [HttpGet("ChecarEmpresa/{id}")]
    [SwaggerOperation(Summary = "Checa uma empresa de acordo com o ID informado", Description = "Retorna as informações da empresa com o id informado.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmpresaDTO>> GetEmpresaId(int id)
    {
        var empresaId = await _empresasService.GetEmpresaIdService(id);

        if (empresaId == null)
        {
            _logger.LogError($"Empresa Id {id} não localizada");
            return NotFound();
        }

        return Ok(empresaId);
    }

    [HttpPost("CriarEmpresa")]
    [SwaggerOperation(Summary = "Adiciona uma empresa ao sistema.", Description = "Retorna a criação de uma empresa.")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpresaDTO>> PostEmpresa(EmpresaDTO empresa)
    {
        var createEmpresa = await _empresasService.CreateEmpresaService(empresa);

        if (createEmpresa == null)
        {
            _logger.LogError("Não foi possível criar a empresa informada.");
            return BadRequest();
        }

        var empresaCriada = _mapper.Map<EmpresaDTO>(createEmpresa);

        return CreatedAtAction(nameof(GetEmpresaId), new { id = createEmpresa.Id }, empresaCriada);
    }

    [HttpPut("EditarEmpresa/{id}")]
    [SwaggerOperation(Summary = "Atualiza as informações de uma empresa", Description = "Edita as informações da empresa.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpresaDTO>> PutEmpresa(int id, EmpresaDTO empresa)
    {
        var updateEmpresa = await _empresasService.UpdateEmpresaService(id, empresa);

        if (updateEmpresa == null)
        {
            _logger.LogError("Não foi possível atualizar a empresa informada.");
            return BadRequest();
        }

        return Ok(updateEmpresa);
    }

    [HttpDelete("DeletarEmpresa/{id}")]
    [SwaggerOperation(Summary = "Remove uma empresa do sistema.", Description = "Apaga a empresa desejada.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteEmpresa(int id)
    {
        var deleteEmpresa = await _empresasService.DeleteEmpresaService(id);

        if (deleteEmpresa == null)
        {
            _logger.LogError("Não foi possível deletar a empresa informada.");
            return NotFound();
        }

        return Ok(deleteEmpresa);
    }
}
