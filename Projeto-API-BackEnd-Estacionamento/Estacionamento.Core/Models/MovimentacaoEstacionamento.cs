using System.ComponentModel.DataAnnotations;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;

public class MovimentacaoEstacionamento
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O ID do veículo é obrigatório.")]
    public int VeiculoId { get; set; }
    [Required(ErrorMessage = "O tipo da vaga é obrigatório.")]
    public string TipoVaga { get; set; } = string.Empty;
    [Required(ErrorMessage = "A data de entrada é obrigatória.")]
    public DateTime DataEntrada { get; set; }   
    public DateTime? DataSaida { get; set; }
    [Required(ErrorMessage = "O ID do estabelecimento é obrigatório.")]
    public int EstabelecimentoId { get; set; }  
}
 
