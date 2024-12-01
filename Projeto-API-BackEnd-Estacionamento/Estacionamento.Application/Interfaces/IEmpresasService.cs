using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;

public interface IEmpresasService
{
    Task<IEnumerable<EmpresaDTO>> GetAllEmpresasService();
    Task<EmpresaDTO> GetEmpresaIdService(int id);
    Task<EmpresaDTO> UpdateEmpresaService(EmpresaDTO empresa);
    Task<EmpresaDTO> CreateEmpresaService(EmpresaDTO empresa);
    Task<bool> DeleteEmpresaService(int id);
}
