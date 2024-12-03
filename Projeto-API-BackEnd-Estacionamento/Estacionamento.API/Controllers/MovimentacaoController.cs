using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovimentacaoController : ControllerBase
{
    private readonly IMovimentacaoService _movimentacaoService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public MovimentacaoController(IMovimentacaoService movimentacaoService, ILogger<MovimentacaoController> logger, IMapper mapper)
    {
        _movimentacaoService = movimentacaoService;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet("Estacionados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<MovimentacaoEstacionamentoDTO>>> VeiculosEstacionados()
    {
        var estacionados = await _movimentacaoService.GetAllEstacionados();

        return Ok(estacionados);
    }

    [HttpPost("Entrada")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> RegistrarEntrada([FromBody] MovimentacaoEstacionamentoDTO entrada)
    {
        var registro = await _movimentacaoService.RegistrarEntrada(entrada.PlacaVeiculo, entrada.TipoVeiculo);

        return Ok(registro);
    }

    [HttpPost("Saida/{id}/{placa}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovimentacaoEstacionamentoDTO>> RegistrarSaida(int id, string placa)
    {
        var registro = await _movimentacaoService.RegistrarSaida(id, placa);

        return Ok(registro);
    }
}
