using System.ComponentModel.DataAnnotations;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;

public class VeiculosDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Obrigatório informar a marca do veículo.")]
    [MaxLength(50, ErrorMessage = "A marca pode ter no máximo 50 caracteres.")]
    public string Marca { get; set; } = string.Empty;

    [Required(ErrorMessage = "Obrigatório informar o modelo do veículo.")]
    [MaxLength(50, ErrorMessage = "O modelo pode ter no máximo 50 caracteres.")]
    public string Modelo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Obrigatório informar a cor do veículo.")]
    [MaxLength(30, ErrorMessage = "A cor pode ter no máximo 30 caracteres.")]
    public string Cor { get; set; } = string.Empty;

    [Required(ErrorMessage = "É obrigatório informar a Placa.")]
    [RegularExpression(@"^[A-Z]{3}-\d{4}$", ErrorMessage = "Placa inválida. Use o formato padrão: XXX-0000.")]
    public string Placa { get; set; } = string.Empty;

    [Required(ErrorMessage = "Obrigatório informar o tipo do veículo.")]
    [MaxLength(20, ErrorMessage = "O tipo do veículo pode ter no máximo 20 caracteres.")]
    public string Tipo { get; set; } = string.Empty;
}
