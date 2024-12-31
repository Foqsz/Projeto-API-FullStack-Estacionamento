using System.ComponentModel.DataAnnotations;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;

public class EmpresaDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
    [MinLength(5, ErrorMessage = "O nome deve ter pelo menos 5 caracteres.")]
    [MaxLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CNPJ é obrigatório.")]
    [RegularExpression(@"^\d{14}$", ErrorMessage = "CNPJ inválido. Deve conter 14 números.")]
    public string CNPJ { get; set; } = string.Empty;

    [Required(ErrorMessage = "O Endereço é obrigatório.")]
    [MaxLength(200, ErrorMessage = "O endereço pode ter no máximo 200 caracteres.")]
    public string Endereco { get; set; } = string.Empty;

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [Phone(ErrorMessage = "Telefone inválido. Use o formato (XX) XXXXX-XXXX.")]
    [MaxLength(15, ErrorMessage = "O telefone pode ter no máximo 15 caracteres.")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informar a quantidade de vagas para motos é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade de vagas para motos deve ser maior que 0.")]
    public int qVagasMotos { get; set; }

    [Required(ErrorMessage = "Informar a quantidade de vagas para carros é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade de vagas para carros deve ser maior que 0.")]
    public int qVagasCarros { get; set; }
}
