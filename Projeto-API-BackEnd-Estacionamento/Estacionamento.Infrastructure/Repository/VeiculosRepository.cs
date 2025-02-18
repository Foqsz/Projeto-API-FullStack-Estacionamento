﻿using Microsoft.EntityFrameworkCore;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Data;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository;

public class VeiculosRepository : IVeiculosRepository
{
    private readonly EstacionamentoDbContext _context;
    private readonly ILogger _logger;

    public VeiculosRepository(EstacionamentoDbContext context, ILogger<VeiculosRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Veiculos>> GetAllVeiculos()
    {
        return await _context.Veiculos.ToListAsync();
    }

    public async Task<Veiculos> GetVeiculoId(int id)
    {
        _logger.LogInformation($"Veiculo Id: {id}");
        return await _context.Veiculos.Where(v => v.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Veiculos> CreateVeiculo(Veiculos veiculo)
    {
        var checkVeiculoExiste = await _context.Veiculos.AnyAsync(v => v.Placa == veiculo.Placa);

        if (checkVeiculoExiste)
        {
            _logger.LogError("CREATE VEICULO: VEICULO já existente no banco de dados. ");
            throw new InvalidOperationException($"VEICULO de placa {veiculo.Placa} já existente no banco de dados.");
        }

        await _context.Veiculos.AddAsync(veiculo);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation($"VEICULO CREATED: {veiculo.Placa}");
        return veiculo;
    }

    public async Task<Veiculos> UpdateVeiculo(int id, Veiculos veiculo)
    {
        if (id != veiculo.Id)
        {
            _logger.LogError("UPDATE: O ID da URL não corresponde ao ID da empresa informada no corpo da requisição.");
            throw new InvalidOperationException("O ID da URL deve ser o mesmo que o ID da empresa.");
        }

        var veiculoUpdate = await _context.Veiculos.FindAsync(veiculo.Id);

        if (veiculoUpdate == null)
        {
            _logger.LogError("UPDATE VEICULO: VEICULO não localizado no banco de dados.");
            throw new KeyNotFoundException("VEICULO não encontrada.");
        }

        var placaExiste = await _context.Veiculos.AnyAsync(v => v.Placa == veiculo.Placa && v.Id != veiculo.Id);

        if (placaExiste)
        {
            _logger.LogError("UPDATE VEICULO: Já existe um veiculo com a Placa informada.");
            throw new InvalidOperationException("Já existe um veiculo com essa Placa.");
        }

        _context.Entry(veiculoUpdate).State = EntityState.Detached;

        _context.Veiculos.Update(veiculo);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"VEICULO UPDATED: {veiculo.Placa}");
        return veiculo;
    }

    public async Task<bool> DeleteVeiculo(int id)
    {
        var removeVeiculo = await _context.Veiculos.FindAsync(id);

        if (removeVeiculo == null)
        {
            _logger.LogError("DELETE VEICULO: VEICULO não localizado no banco de dados.");
            throw new KeyNotFoundException("VEICULO não encontrado. Não foi posssível deletar.");
        }

        // Remover o veículo dos estacionados antes de deletar
        var veiculoEstacionado = await _context.movimentacaoEstacionamento
            .FirstOrDefaultAsync(v => v.PlacaVeiculo == removeVeiculo.Placa);

        if (veiculoEstacionado != null)
        {
            _context.movimentacaoEstacionamento.Remove(veiculoEstacionado);
            await _context.SaveChangesAsync();  
        }

        _context.Veiculos.Remove(removeVeiculo);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"VEICULO DELETED: {removeVeiculo.Placa}");
        return true;
    }
}
