using AutoMapper;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Services;

public class MovimentacaoService : IMovimentacaoService
{
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public MovimentacaoService(IMovimentacaoRepository movimentacaoRepository, ILogger<MovimentacaoService> logger, IMapper mapper)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MovimentacaoEstacionamentoDTO>> GetAllEstacionados()
    {
        var estacionados = await _movimentacaoRepository.GetAllEstacionados();

        return _mapper.Map<IEnumerable<MovimentacaoEstacionamentoDTO>>(estacionados);
    }

    public async Task<MovimentacaoEstacionamentoDTO> RegistrarEntrada(string placa, string TipoVeiculo)
    {
        var entradaVeiculo = await _movimentacaoRepository.RegistrarEntrada(placa, TipoVeiculo);

        return _mapper.Map<MovimentacaoEstacionamentoDTO>(entradaVeiculo);
    }

    public async Task<MovimentacaoEstacionamentoDTO> RegistrarSaida(int id, string placa)
    {
        var saidaVeiculo = await _movimentacaoRepository.RegistrarSaida(id, placa);

        return _mapper.Map<MovimentacaoEstacionamentoDTO>(saidaVeiculo);
    }

    public async Task<bool> VagaDisponivel(string tipoVeiculo)
    {
        var vagas = _movimentacaoRepository.VagaDisponivel(tipoVeiculo);

        return vagas.Result;
    }
}
