﻿using AutoMapper;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Services;

public class VeiculosService : IVeiculosService
{
    private readonly IVeiculosRepository _veiculosRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public VeiculosService(IVeiculosRepository veiculosRepository, ILogger<VeiculosService> logger, IMapper mapper)
    {
        _veiculosRepository = veiculosRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VeiculosDTO>> GetAllVeiculos()
    {
        var veiculosAll = await _veiculosRepository.GetAllVeiculos();

        if (veiculosAll.Any()) return _mapper.Map<IEnumerable<VeiculosDTO>>(veiculosAll);
        _logger.LogError("Nenhum veiculo registrado no sistema.");
        throw new ArgumentNullException(nameof(veiculosAll), "Não foi localizado nenhum veículo no banco de dados.");

    }

    public async Task<VeiculosDTO> GetVeiculoId(int id)
    {
        var veiculoId = await _veiculosRepository.GetVeiculoId(id);

        if (veiculoId == null)
        {
            throw new Exception("Não foi localizado nenhum veículo com o ID informado.");
        }

        return _mapper.Map<VeiculosDTO>(veiculoId);
    }

    public async Task<VeiculosDTO> CreateVeiculo(VeiculosDTO veiculo)
    {
        var veiculoMap = _mapper.Map<VeiculosDTO, Veiculos>(veiculo);

        var newVeiculo = await _veiculosRepository.CreateVeiculo(veiculoMap);

        return _mapper.Map<VeiculosDTO>(newVeiculo);
    }

    public async Task<VeiculosDTO> UpdateVeiculo(int id, VeiculosDTO veiculo)
    {
        var veiculoMap = _mapper.Map<VeiculosDTO, Veiculos>(veiculo);

        var updateVeiculo = await _veiculosRepository.UpdateVeiculo(id, veiculoMap);

        return _mapper.Map<VeiculosDTO>(updateVeiculo);
    }
    public async Task<bool> DeleteVeiculo(int id)
    {
        var veiculoDelete = await _veiculosRepository.DeleteVeiculo(id);

        return veiculoDelete;
    }
}
