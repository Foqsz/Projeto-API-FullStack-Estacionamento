using Estacionamento_FrontEnd.Estacionamento.Core.Models;

namespace Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface
{
    public interface IVeiculoService
    {
        Task<IEnumerable<VeiculosViewModel>> GetVeiculosAll();
        Task<VeiculosViewModel> GetVeiculoById(int id);
        Task<VeiculosViewModel> PostVeiculo(VeiculosViewModel veiculo);
        Task<VeiculosViewModel> PutVeculo(int id, VeiculosViewModel veiculo);
        Task<bool> DeleteVeiculo(int id);
    }
}
