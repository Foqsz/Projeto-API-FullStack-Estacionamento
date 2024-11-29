using Microsoft.EntityFrameworkCore;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Data;

public class EstacionamentoDbContext : DbContext
{
    public EstacionamentoDbContext(DbContextOptions<EstacionamentoDbContext> options) : base(options)
    {
    }

    public DbSet<Empresa> Empresa { get; set; }
    public DbSet<Veiculos> Veiculos { get; set; }
    public DbSet<MovimentacaoEstacionamento> movimentacaoEstacionamento { get; set; }
}
