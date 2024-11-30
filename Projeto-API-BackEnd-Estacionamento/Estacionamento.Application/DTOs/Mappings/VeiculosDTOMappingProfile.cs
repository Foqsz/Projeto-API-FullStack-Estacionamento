using AutoMapper;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs.Mappings;

public class VeiculosDTOMappingProfile : Profile
{
    public VeiculosDTOMappingProfile()
    {
        CreateMap<Veiculos, VeiculosDTO>().ReverseMap();
    }
}
