using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

public interface IMovimentacaoRepository
{
    Task<MovimentacaoEstacionamento> RegistrarEntrada(string placa, string TipoVeiculo);
    Task<MovimentacaoEstacionamento> RegistrarSaida(int id, string placa);
    Task<IEnumerable<MovimentacaoEstacionamento>> GetAllEstacionados();
}
