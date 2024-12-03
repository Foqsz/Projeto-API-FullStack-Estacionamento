using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;

public interface IMovimentacaoService
{
    Task<MovimentacaoEstacionamentoDTO> RegistrarEntrada(string placa, string TipoVeiculo);
    Task<MovimentacaoEstacionamentoDTO> RegistrarSaida(int id, string placa);
    Task<bool> VagaDisponivel(string tipoVeiculo);
    Task<IEnumerable<MovimentacaoEstacionamentoDTO>> GetAllEstacionados();
}
