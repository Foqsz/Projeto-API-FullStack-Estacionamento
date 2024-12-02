using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

public interface IVeiculosRepository
{
    Task<IEnumerable<Veiculos>> GetAllVeiculos();
    Task<Veiculos> GetVeiculoId(int id);
    Task<Veiculos> UpdateVeiculo(int id, Veiculos veiculo);
    Task<Veiculos> CreateVeiculo(Veiculos veiculo);
    Task<bool> DeleteVeiculo(int id);
}
