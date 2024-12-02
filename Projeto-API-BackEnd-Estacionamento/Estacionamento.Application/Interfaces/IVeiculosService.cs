using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;

public interface IVeiculosService
{
    Task<IEnumerable<VeiculosDTO>> GetAllVeiculos();
    Task<VeiculosDTO> GetVeiculoId(int id);
    Task<VeiculosDTO> UpdateVeiculo(int id, VeiculosDTO veiculo);
    Task<VeiculosDTO> CreateVeiculo(VeiculosDTO veiculo);
    Task<bool> DeleteVeiculo(int id);
}
