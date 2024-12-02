using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VeiculosController : ControllerBase
{
    private readonly IVeiculosService _veiculosService;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public VeiculosController(IVeiculosService veiculosService, IMapper mapper, ILogger<VeiculosController> logger)
    {
        _veiculosService = veiculosService;
        _mapper = mapper;
        _logger = logger;
    }
     
    [HttpGet("ListarVeiculos")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<VeiculosDTO>>> GetVeiculosAll()
    {
        var listarVeiculos = await _veiculosService.GetAllVeiculos();

        if (listarVeiculos == null)
        {
            _logger.LogError("Não foi possível listar os veiculos.");
            return NotFound();
        }

        return Ok(listarVeiculos);
    }
     
    [HttpGet("ChecarVeiculo/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VeiculosDTO>> GetVeiculoById(int id)
    {
        var veiculoId = await _veiculosService.GetVeiculoId(id);

        if (veiculoId == null)
        {
            _logger.LogError($"Não foi possível listar o veiculo id {id}, não localizado..");
            return NotFound();
        }

        return Ok(veiculoId);
    }
     
    [HttpPost("CadastrarVeiculo")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VeiculosDTO>> PostVeiculo(VeiculosDTO veiculo)
    {
        var registerVeiculo = await _veiculosService.CreateVeiculo(veiculo);

        if (registerVeiculo == null)
        {
            _logger.LogError("Não foi possível cadastrar o veiculo.");
            return BadRequest();
        }

        return Ok(registerVeiculo);
    }
     
    [HttpPut("AtualizarVeiculo/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VeiculosDTO>> PutVeiculo(int id, VeiculosDTO veiculo)
    {
        var putVeiculo = await _veiculosService.UpdateVeiculo(id,veiculo);

        if (putVeiculo == null)
        {
            _logger.LogError("Não foi possível atualizar o veiculo.");
            return BadRequest();
        }

        return Ok(putVeiculo);
    }
     
    [HttpDelete("DeletarVeiculo/{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VeiculosDTO>> DeleteVeiculo(int id)
    {
        var deleteVeiculo = await _veiculosService.DeleteVeiculo(id);

        if (deleteVeiculo == null)
        {
            _logger.LogError($"Não foi possível deletar o veiculo ID {id}.");
            return NotFound();
        }

        return Ok(deleteVeiculo);
    }
}
