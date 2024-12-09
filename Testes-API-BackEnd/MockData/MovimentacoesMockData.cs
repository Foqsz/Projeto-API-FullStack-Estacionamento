using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes_API_BackEnd.MockData;

public class MovimentacoesMockData
{
    public static List<MovimentacaoEstacionamentoDTO> GetMovimentacoes()
    {
        return new List<MovimentacaoEstacionamentoDTO>
        {
            new MovimentacaoEstacionamentoDTO
            {
                Id = 1,
                PlacaVeiculo = "1QWDSA23",
                HoraEntrada = DateTime.Now,
                HoraSaida = DateTime.Now.AddMinutes(10),
                TipoVeiculo = "Sedan"
            }
        };
    }

    public static List<MovimentacaoEstacionamentoDTO> GetMovimentacoesNull()
    {
        return new List<MovimentacaoEstacionamentoDTO>();
    }
}
