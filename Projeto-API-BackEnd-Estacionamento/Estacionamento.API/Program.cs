using Microsoft.EntityFrameworkCore;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs.Mappings;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Services;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Data;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner

builder.Services.AddControllers();
 
builder.Services.AddOpenApi();  // padrão v1.json

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

// Configuração do AutoMapper
builder.Services.AddAutoMapper(typeof(EmpresaDTOMappingProfile));
builder.Services.AddAutoMapper(typeof(VeiculosDTOMappingProfile));
builder.Services.AddAutoMapper(typeof(MovimentacaoEstacionamentoDTOMappingProfile));

var app = builder.Build();

// Configuração do pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();  
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
