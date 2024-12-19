using Microsoft.EntityFrameworkCore;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Data;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly EstacionamentoDbContext _context;
    private readonly ILogger _logger;

    public MovimentacaoRepository(EstacionamentoDbContext context, ILogger<MovimentacaoRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MovimentacaoEstacionamento>> GetAllEstacionados()
    {
        var veiculosEstacionados = await _context.movimentacaoEstacionamento.ToListAsync();

        if (!veiculosEstacionados.Any())
        {
            _logger.LogError("Nenhum veiculo estacionado.");
            throw new Exception("Nenhum veículo estacionado.");
        }

        _logger.LogInformation("Veiculos estacionados listados.");
        return veiculosEstacionados;
    }

    public async Task<MovimentacaoEstacionamento> RegistrarEntrada(string placa, string TipoVeiculo)
    {
        var registro = new MovimentacaoEstacionamento()
        {
            PlacaVeiculo = placa,
            HoraEntrada = DateTime.Now,
            TipoVeiculo = TipoVeiculo
        };

        var checkVeiculo = await _context.Veiculos.FirstOrDefaultAsync(r => r.Placa == placa) ??
            throw new Exception($"Veículo com a placa '{placa}' não foi encontrado no registro geral.");

        registro.HoraSaida = DateTime.Now;

        _context.movimentacaoEstacionamento.Add(registro);
        await _context.SaveChangesAsync();

        return registro;
    }

    public async Task<MovimentacaoEstacionamento> RegistrarSaida(int id, string placa)
    {
        var registro = await _context.movimentacaoEstacionamento
            .FirstOrDefaultAsync(r => r.PlacaVeiculo == placa && r.Id == id);

        var checkRegistroDb = await _context.Veiculos.FirstOrDefaultAsync(r => r.Placa == placa) ??
            throw new Exception($"Veículo com a placa '{placa}' e id '{id}' não foi encontrado no registro geral.");

        if (registro == null)
        {
            throw new Exception($"Veículo com a placa '{placa}' e id '{id}' não foi encontrado.");
        }

        registro.HoraSaida = DateTime.Now;

        _context.movimentacaoEstacionamento.Remove(registro);
        await _context.SaveChangesAsync();

        return registro;
    }
}
