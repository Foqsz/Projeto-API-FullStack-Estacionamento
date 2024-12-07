using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes_API_BackEnd.MockData
{
    public class EmpresasMockData
    {
        public static List<EmpresaDTO> GetEmpresas()
        {
            return new List<EmpresaDTO>
            {
                new EmpresaDTO
                {
                    Id = 1,
                    Nome = "VV Alto Center",
                    CNPJ = 189647772,
                    Endereco = "Rua Flamengo",
                    Telefone = "83994195048",
                    qVagasMotos = 10,
                    qVagasCarros = 10
                },
                new EmpresaDTO
                {
                    Id = 2,
                    Nome = "VV Auto Premium",
                    CNPJ = 238476552,
                    Endereco = "Avenida Paulista",
                    Telefone = "83999988877",
                    qVagasMotos = 15,
                    qVagasCarros = 20
                },
                new EmpresaDTO
                {
                    Id = 3,
                    Nome = "VV Service Hub",
                    CNPJ = 194867412,
                    Endereco = "Rua das Palmeiras",
                    Telefone = "83996655443",
                    qVagasMotos = 5,
                    qVagasCarros = 12
                }
            };
        }

        public static List<EmpresaDTO> GetEmpresasNull()
        {
            return new List<EmpresaDTO>();
        }
    }
}
