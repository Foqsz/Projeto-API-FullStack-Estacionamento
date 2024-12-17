using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Estacionamento_FrontEnd.Estacionamento.Core.Models
{
    public class EmpresaViewModel
    { 
        public int Id { get; set; }
        [JsonPropertyName("Nome")]
        [Required(ErrorMessage = "O nome da empresa é obrigatório")]
        public string Nome { get; set; } = string.Empty;
        [JsonPropertyName("CNPJ")]
        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        public int CNPJ { get; set; }
        [JsonPropertyName("Endereco")]
        [Required(ErrorMessage = "O Endereço é obrigatório.")]
        public string Endereco { get; set; } = string.Empty;
        [JsonPropertyName("Telefone")]
        [Required(ErrorMessage = "O telefone é obrigatorio.")]
        [Phone(ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; } = string.Empty;
        [JsonPropertyName("qVagasMotos")]
        [Required(ErrorMessage = "Informar a quantidade de vagas para motos é obrigatório.")]
        public int qVagasMotos { get; set; }
        [JsonPropertyName("qVagasCarros")]
        [Required(ErrorMessage = "Informar a quantidade de vagas para carros é obrigatório.")]
        public int qVagasCarros { get; set; }
    }
}
