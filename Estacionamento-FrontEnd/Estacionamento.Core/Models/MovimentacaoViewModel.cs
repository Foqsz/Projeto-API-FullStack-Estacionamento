using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Estacionamento_FrontEnd.Estacionamento.Core.Models
{
    public class MovimentacaoViewModel
    {
        public int Id { get; set; }
        [JsonPropertyName("PlacaVeiculo")]
        [Required(ErrorMessage = "A Placa do veículo é obrigatória.")]
        public string PlacaVeiculo { get; set; }
        [JsonPropertyName("HoraEntrada")]
        [Required(ErrorMessage = "Informar a hora de entrada do veiculo é obrigatório.")]
        public DateTime HoraEntrada { get; set; }
        [JsonPropertyName("HoraSaida")]
        [Required(ErrorMessage = "Informar a hora de saida do veiculo é obrigatório.")]
        public DateTime? HoraSaida { get; set; }
        [JsonPropertyName("TipoVeiculo")]
        public string TipoVeiculo { get; set; }
    }
}
