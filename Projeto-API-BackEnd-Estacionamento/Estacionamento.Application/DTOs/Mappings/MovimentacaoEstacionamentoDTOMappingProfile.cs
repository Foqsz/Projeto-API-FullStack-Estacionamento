using AutoMapper;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs.Mappings;

public class MovimentacaoEstacionamentoDTOMappingProfile : Profile
{
    public MovimentacaoEstacionamentoDTOMappingProfile()
    {
        CreateMap<MovimentacaoEstacionamento, MovimentacaoEstacionamentoDTO>().ReverseMap();
    }
}
