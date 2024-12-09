using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;

namespace Testes_API_BackEnd.MockData;

public class VeiculosMockData
{
    public static List<VeiculosDTO> GetVeiculos()
    {
        return new List<VeiculosDTO>()
        {
            new VeiculosDTO
            {
                Id = 1,
                Marca = "Fiat",
                Modelo = "COMPASS",
                Cor = "Branco",
                Placa = "A47WE8F",
                Tipo = "Quatro Portas"
            },
            new VeiculosDTO
            {
                Id = 2,
                Marca = "Toyota",
                Modelo = "Corolla",
                Cor = "Preto",
                Placa = "B23XT5G",
                Tipo = "Sedan"
            },
            new VeiculosDTO
            {
                Id = 3,
                Marca = "Chevrolet",
                Modelo = "Onix",
                Cor = "Azul",
                Placa = "C89YU1H",
                Tipo = "Hatchback"
            },
        };
    }

    public static List<VeiculosDTO> GetVeiculosNull()
    {
        return new List<VeiculosDTO>();
    }
}
