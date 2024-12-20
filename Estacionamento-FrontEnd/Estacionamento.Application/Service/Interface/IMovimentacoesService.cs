using Estacionamento_FrontEnd.Estacionamento.Core.Models;

namespace Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface;

public interface IMovimentacoesService
{
    Task<IEnumerable<MovimentacaoViewModel>> GetEstacionadosAll();
    Task<MovimentacaoViewModel> RegistrarEntrada(string placa, string tipoVeiculo);
    Task<bool> RegistrarSaida(int id, string placa);
}
