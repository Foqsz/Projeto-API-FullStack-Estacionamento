using Microsoft.EntityFrameworkCore;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Data;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository
{
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
            return await _context.Veiculos.Where(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Veiculos> CreateVeiculo(Veiculos veiculo)
        {
            var checkVeiculoExiste = await _context.Veiculos.AnyAsync(v => v.Placa == veiculo.Placa);

            if (checkVeiculoExiste != null)
            {
                _logger.LogError("CREATE VEICULO: VEICULO já existente no banco de dados. ");
                throw new InvalidOperationException($"VEICULO de placa {veiculo.Placa} já existente no banco de dados.");
            }

            await _context.Veiculos.AddAsync(veiculo);
            await _context.SaveChangesAsync();
            return veiculo;
        }

        public async Task<Veiculos> UpdateVeiculo(Veiculos veiculo)
        {
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

            _context.Veiculos.Update(veiculo);
            await _context.SaveChangesAsync();

            return veiculo;
        }

        public async Task<bool> DeleteVeiculo(int id)
        {
            var removeVeiculo = await _context.Veiculos.FindAsync(id);

            if (removeVeiculo == null)
            {
                _logger.LogError("DELETE VEICULO: VEICULO não localizado no banco de dados.");
                throw new KeyNotFoundException("VEICULO não encontrado.");
            }

            _context.Veiculos.Remove(removeVeiculo);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
