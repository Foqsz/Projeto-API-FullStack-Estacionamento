using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

public interface IEmpresasRepository
{
    Task<IEnumerable<Empresa>> GetAllEmpresas();
    Task<Empresa> GetEmpresaId(int id);
    Task<Empresa> UpdateEmpresa(int id, Empresa empresa);
    Task<Empresa> CreateEmpresa(Empresa empresa);
    Task<bool> DeleteEmpresa(int id);
}
