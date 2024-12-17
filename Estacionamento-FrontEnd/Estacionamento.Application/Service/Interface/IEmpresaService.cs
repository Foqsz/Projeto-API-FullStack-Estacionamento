using Estacionamento_FrontEnd.Estacionamento.Core.Models;

namespace Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface
{
    public interface IEmpresaService
    {
        Task<IEnumerable<EmpresaViewModel>> GetEmpresaAll();
        Task<EmpresaViewModel> GetEmpresaById(int id);
        Task<EmpresaViewModel> PostEmpresa(EmpresaViewModel empresa);
        Task<EmpresaViewModel> PutEmpresa(int id, EmpresaViewModel empresa);
        Task<bool> DeletarEmpresa(int id);
    }
}
