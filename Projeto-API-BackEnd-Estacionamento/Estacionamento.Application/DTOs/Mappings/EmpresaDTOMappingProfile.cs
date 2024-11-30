using AutoMapper;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs.Mappings;

public class EmpresaDTOMappingProfile : Profile
{
    public EmpresaDTOMappingProfile()
    {
        CreateMap<Empresa, EmpresaDTO>().ReverseMap();
    }
}
