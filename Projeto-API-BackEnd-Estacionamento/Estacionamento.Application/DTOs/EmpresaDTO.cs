using System.ComponentModel.DataAnnotations;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;

public class EmpresaDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome da empresa é obrigatório")]
    public string Nome { get; set; } = string.Empty;
    [Required(ErrorMessage = "O CNPJ é obrigatório.")]
    public int CNPJ { get; set; }
    [Required(ErrorMessage = "O Endereço é obrigatório.")]
    public string Endereco { get; set; } = string.Empty;
    [Required(ErrorMessage = "O telefone é obrigatorio.")]
    [Phone(ErrorMessage = "Telefone inválido")]
    public string Telefone { get; set; } = string.Empty;
    [Required(ErrorMessage = "Informar a quantidade de vagas para motos é obrigatório.")]
    public int qVagasMotos { get; set; }
    [Required(ErrorMessage = "Informar a quantidade de vagas para carros é obrigatório.")]
    public int qVagasCarros { get; set; }
}
