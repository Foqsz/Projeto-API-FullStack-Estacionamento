namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;

public record User(

    int Id,
    string Email,
    string Password,
    string[] Roles
);