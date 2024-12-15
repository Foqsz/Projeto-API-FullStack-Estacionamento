using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

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

    #region Listar todos os veículos no sistema
    /// <summary>
    /// Listar todos os veículos da empresa.
    /// </summary>
    /// <returns>Retorna todos os veículos.</returns>
    [HttpGet("ListarVeiculos")]
    [SwaggerOperation(Summary = "Lista todos os veículos.", Description = "Retorna todos os veículos do banco de dados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<VeiculosDTO>>> GetVeiculosAll()
    {
        var listarVeiculos = await _veiculosService.GetAllVeiculos();

        return Ok(listarVeiculos);

    }
    #endregion
    
    #region Checar veículo pelo ID
    /// <summary>
    /// Lista um veículo de acordo com o ID informado.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna a informação do veiculo id que foi informado.</returns>
    [HttpGet("ChecarVeiculo/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Checa um veículo de acordo com o id informado.", Description = "Retorna o veiculo de ID informado.")]
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

        _logger.LogInformation($"Veiculo id {id} checado com sucesso.");
        return Ok(veiculoId);
    }
    #endregion
    
    #region Cadastrar Veículo
    /// <summary>
    /// Cadastra um veiculo no sistema.
    /// </summary>
    /// <param name="veiculo"></param>
    /// <returns>Retorna a criação de um veiculo.</returns>
    [HttpPost("CadastrarVeiculo")]
    [Authorize]
    [SwaggerOperation(Summary = "Cadastra um veiculo no sistema.", Description = "Adiciona um veiculo no banco de dados.")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VeiculosDTO>> PostVeiculo(VeiculosDTO veiculo)
    {
        var registerVeiculo = await _veiculosService.CreateVeiculo(veiculo);

        if (registerVeiculo == null)
        {
            _logger.LogError("Não foi possível cadastrar o veiculo.");
            return NotFound();
        }

        _logger.LogInformation("Veiculo cadastrado com sucesso no sistema.");
        return CreatedAtAction(nameof(GetVeiculoById), new { id = registerVeiculo.Id }, registerVeiculo);
    }
    #endregion
    
    #region Atualizar Veículo
    /// <summary>
    /// Atualiza um veiculo no banco de dados.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="veiculo"></param>
    /// <returns>Retorna um veiculo atualizado.</returns>
    [HttpPut("AtualizarVeiculo/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Atualizar as informações de um veículo.", Description = "Retorna um veiculo atualizado.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VeiculosDTO>> PutVeiculo(int id, VeiculosDTO veiculo)
    {
        var putVeiculo = await _veiculosService.GetVeiculoId(id);

        if (putVeiculo == null)
        {
            _logger.LogError("Não foi possível atualizar o veiculo.");
            return NotFound("Não foi possível atualizar o veiculo.");
        }

        if (id != putVeiculo.Id)
        {
            _logger.LogError("O ID do veiculo não corresponde ao ID fornecido.");
            return BadRequest("O ID do veiculo não corresponde ao ID fornecido.");
        }

        var updateVeiculo = await _veiculosService.UpdateVeiculo(id, veiculo);

        var veiculoAtualizado = _mapper.Map<VeiculosDTO>(updateVeiculo);

        _logger.LogInformation("O veiculo foi atualizado com sucesso.");
        return Ok(veiculoAtualizado);
    }
    #endregion

    #region Deletar Veículo
    /// <summary>
    /// Deleta um veiculo no banco de dados.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna o veiculo deletado.</returns>
    [HttpDelete("DeletarVeiculo/{id}")]
    [Authorize]
    [SwaggerOperation(Summary = "Deleta um veiculo.", Description = "Retorna um veiculo deletado.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteVeiculo(int id)
    {   
        var deleteVeiculo = await _veiculosService.DeleteVeiculo(id);

        _logger.LogInformation("Veiculo deletado com sucesso.");
        return Ok("Veículo deletado com sucesso.");
    }
    #endregion

}
