using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs.Mappings;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Services;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Data;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;
using System.Text;
using Microsoft.Extensions.Caching.Hybrid;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container
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

    // Configuração de segurança para JWT no Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])), // Chave secreta usada para assinar o JWT
            ClockSkew = TimeSpan.Zero
        };
    });


// Configuração da conexão com o banco de dados
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EstacionamentoDbContext>(options =>
    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

// Registrando os serviços de repositório e serviços
builder.Services.AddScoped<IEmpresasRepository, EmpresasRepository>();
builder.Services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
builder.Services.AddScoped<IVeiculosRepository, VeiculosRepository>();

builder.Services.AddScoped<IEmpresasService, EmpresasService>();
builder.Services.AddScoped<IVeiculosService, VeiculosService>();
builder.Services.AddScoped<IMovimentacaoService, MovimentacaoService>();

//cache

builder.Services.AddMemoryCache();

//cache distribuido usando o redis no docker
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

//cache hibrido

#pragma warning disable EXTEXP0018
builder.Services.AddHybridCache(options =>
{
    options.MaximumPayloadBytes = 1024 * 1024;
    options.MaximumKeyLength = 1024;
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromSeconds(20),
        LocalCacheExpiration = TimeSpan.FromSeconds(20),
    };
});
#pragma warning restore EXTEXP0018

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();