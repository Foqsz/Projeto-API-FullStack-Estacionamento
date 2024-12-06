using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Services;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("generate-token")]
    public IActionResult GenerateToken()
    {
        // Simula um usuário genérico
        var user = new User(1, "foqs@net", "123", new[] { "User", "Admin" });

        // Gera o token
        var token = _tokenService.Generate(user);

        // Retorna o token
        return Ok(new { Token = token });
    }

}



