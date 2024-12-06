using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs.Mappings;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Services;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Data;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddControllers();

// Adiciona o Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Estacionamento",
        Version = "v1",
        Description = "API Desafio Back-end .NET",
        Contact = new OpenApiContact
        {
            Name = "Victor Vinicius Alves de L. Souza",
            Url = new Uri("https://www.linkedin.com/in/victorvinicius/")
        }
    });
});

// Configuração da conexão com o banco de dados
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EstacionamentoDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

// Registrando os serviços de repositório e serviço
builder.Services.AddScoped<IEmpresasRepository, EmpresasRepository>();
builder.Services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
builder.Services.AddScoped<IVeiculosRepository, VeiculosRepository>();

builder.Services.AddScoped<IEmpresasService, EmpresasService>();
builder.Services.AddScoped<IVeiculosService, VeiculosService>();
builder.Services.AddScoped<IMovimentacaoService, MovimentacaoService>();

//token jwt
builder.Services.AddTransient<TokenService>();

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(EmpresaDTOMappingProfile));
builder.Services.AddAutoMapper(typeof(VeiculosDTOMappingProfile));
builder.Services.AddAutoMapper(typeof(MovimentacaoEstacionamentoDTOMappingProfile));

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    // Ativa o Swagger no ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;  // Isso coloca a interface do Swagger na raiz do aplicativo
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
