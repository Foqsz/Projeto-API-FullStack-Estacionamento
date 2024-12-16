using System.ComponentModel.DataAnnotations;

namespace Estacionamento_FrontEnd.Estacionamento.Core.Models
{
    public class MovimentacaoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A Placa do veículo é obrigatória.")]
        public string PlacaVeiculo { get; set; } // Relaciona com o veículo 
        public DateTime HoraEntrada { get; set; }
        public DateTime? HoraSaida { get; set; }
        public string TipoVeiculo { get; set; }
    }
}
