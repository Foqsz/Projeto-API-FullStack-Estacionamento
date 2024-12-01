using AutoMapper;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Infrastructure.Repository.Interface;

namespace Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Services;

public class EmpresasService : IEmpresasService
{
    private readonly IEmpresasRepository _empresasRepository;
    private readonly Mapper _mapper;
    private readonly ILogger _logger;

    public EmpresasService(IEmpresasRepository empresasRepository, Mapper mapper, ILogger<EmpresasService> logger)
    {
        _empresasRepository = empresasRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<EmpresaDTO>> GetAllEmpresasService()
    {
        var allEmpresas = await _empresasRepository.GetAllEmpresas();

        return _mapper.Map<IEnumerable<EmpresaDTO>>(allEmpresas);
    }

    public async Task<EmpresaDTO> GetEmpresaIdService(int id)
    {
        var checkEmpresaId = await _empresasRepository.GetEmpresaId(id);

        if (checkEmpresaId == null)
        {
            _logger.LogError($"GET EMPRESA ID SERVICE: Não localizado a empresa de id {id}.");
            throw new ArgumentException($"Empresa de id {id} não localizada.");
        }
        return _mapper.Map<EmpresaDTO>(checkEmpresaId);
    }

    public async Task<EmpresaDTO> CreateEmpresaService(EmpresaDTO empresa)
    {
        var empresaMapper = _mapper.Map<EmpresaDTO, Empresa>(empresa);

        if (empresa == null)
        {
            _logger.LogError("CREATE EMPRESA SERVICE: não foi possível criar essa empresa.");
            throw new ArgumentException("Não foi possível criar essa empresa.");
        }

        var createEmpresa = await _empresasRepository.CreateEmpresa(empresaMapper);

        return _mapper.Map<Empresa, EmpresaDTO>(createEmpresa);

    }

    public async Task<EmpresaDTO> UpdateEmpresaService(EmpresaDTO empresa)
    {
        var empresaMapper = _mapper.Map<EmpresaDTO, Empresa>(empresa);

        if (empresaMapper == null)
        {
            _logger.LogError("UPDATE EMPRESA SERVICE: Não foi possível fazer update nessa empresa.");
            throw new ArgumentException("Não foi possível fazer update nessa empresa.");
        }

        var updateEmpresa = await _empresasRepository.UpdateEmpresa(empresaMapper);

        return _mapper.Map<Empresa, EmpresaDTO>(updateEmpresa);
    }

    public async Task<bool> DeleteEmpresaService(int id)
    {
        var deleteEmpresa = await _empresasRepository.DeleteEmpresa(id);

        if (deleteEmpresa == true)
        {
            return true;
        }
        return false;
    }
}
