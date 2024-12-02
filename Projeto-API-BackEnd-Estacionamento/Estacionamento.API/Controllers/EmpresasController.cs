using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmpresaDTO>> PutEmpresa(EmpresaDTO empresa)
    {
        var updateEmpresa = await _empresasService.UpdateEmpresaService(empresa);

        if (updateEmpresa == null)
        {
            _logger.LogError("Não foi possível atualizar a empresa informada.");
            return BadRequest();
        }

        return Ok(updateEmpresa);
    }

    [HttpDelete("DeletarEmpresa/{id}")]
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
