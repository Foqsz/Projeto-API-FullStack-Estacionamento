using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Data;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository;

public class EmpresasRepository : IEmpresasRepository
{
    private readonly EstacionamentoDbContext _context;
    private readonly ILogger _logger;

    public EmpresasRepository(EstacionamentoDbContext context, ILogger<EmpresasRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Empresa>> GetAllEmpresas()
    {
        _logger.LogInformation("Get all empresas");
        return await _context.Empresa.ToListAsync();
    }

    public async Task<Empresa> GetEmpresaId(int id)
    {
        _logger.LogInformation($"Get empresa id: {id}");
        return await _context.Empresa.Where(e => e.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Empresa> CreateEmpresa(Empresa empresa)
    {
        var checkEmpresaExiste = await _context.Empresa.AnyAsync(e => e.CNPJ == empresa.CNPJ);

        if (checkEmpresaExiste)
        {
            _logger.LogError("CREATE: Empresa já existente no banco de dados. ");
            throw new InvalidOperationException("Empresa já existente no banco de dados.");
        }

        await _context.Empresa.AddAsync(empresa);
        await _context.SaveChangesAsync();
        
        _logger.LogInformation("CREATE: Empresa no banco de dados.");
        return empresa;

    }
    public async Task<Empresa> UpdateEmpresa(int id, Empresa empresa)
    {
        if (id != empresa.Id)
        {
            _logger.LogError("UPDATE: O ID da URL não corresponde ao ID da empresa informada no corpo da requisição.");
            throw new InvalidOperationException("O ID da URL deve ser o mesmo que o ID da empresa.");
        }

        var empresaExistente = await _context.Empresa.FindAsync(empresa.Id);

        if (empresaExistente == null)
        {
            _logger.LogError("UPDATE: Empresa não localizada no banco de dados.");
            throw new KeyNotFoundException("Empresa não encontrada.");
        }

        var cnpjExiste = await _context.Empresa.AnyAsync(e => e.CNPJ == empresa.CNPJ && e.Id != empresa.Id);

        if (cnpjExiste)
        {
            _logger.LogError("UPDATE: Já existe uma empresa com o CNPJ informado.");
            throw new InvalidOperationException("Já existe uma empresa com este CNPJ.");
        }

        _context.Entry(empresaExistente).State = EntityState.Detached;

        // Atualiza a entidade
        _context.Empresa.Update(empresa);
        await _context.SaveChangesAsync();

        _logger.LogInformation("UPDATE: Empresa no banco de dados.");
        return empresa;
    }
    
    public async Task<bool> DeleteEmpresa(int id)
    {
        var removeEmpresa = await _context.Empresa.FindAsync(id);

        if (removeEmpresa == null)
        {
            _logger.LogError("DELETE: Empresa não localizada no banco de dados.");
            throw new KeyNotFoundException("Empresa não encontrada no banco de dados.");
        }

        _context.Empresa.Remove(removeEmpresa);
        await _context.SaveChangesAsync();

        _logger.LogInformation("DELETE: Empresa no banco de dados.");
        return true;
    }
}
