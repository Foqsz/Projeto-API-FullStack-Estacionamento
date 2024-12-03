using System.ComponentModel.DataAnnotations;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;

public class MovimentacaoEstacionamentoDTO
{
    [Required(ErrorMessage = "O ID do veículo é obrigatório.")]
    public int Id { get; set; }
    [Required(ErrorMessage = "A Placa do veículo é obrigatória.")]
    public string PlacaVeiculo { get; set; } // Relaciona com o veículo
    [Required(ErrorMessage = "A Hora de entrada do veículo é obrigatória.")]
    public DateTime HoraEntrada { get; set; }
    [Required(ErrorMessage = "A Hora de saída do veículo é obrigatória.")]
    public DateTime? HoraSaida { get; set; }
    [Required(ErrorMessage = "O Tipo do veículo é obrigatório.")]
    public string TipoVeiculo { get; set; }
}
