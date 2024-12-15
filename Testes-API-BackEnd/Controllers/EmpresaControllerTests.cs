using AutoMapper;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.API.Controllers;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Services;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes_API_BackEnd.MockData;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Testes_API_BackEnd.Controllers;

public class EmpresaControllerTests
{
    [Fact]
    public async Task GetTodasEmpresasAsync_ShouldReturn200Status()
    {
        /// Arrange
        var empresaService = new Mock<IEmpresasService>();
        empresaService.Setup(_ => _.GetAllEmpresasService()).ReturnsAsync(EmpresasMockData.GetEmpresas());

        var logger = new Mock<ILogger<EmpresasController>>();
        var mapper = new Mock<IMapper>();

        var sut = new EmpresasController(empresaService.Object, logger.Object, mapper.Object);

        /// Act
        var result = await sut.GetEmpresasAll();

        /// Assert
        var okResult = result.Result as OkObjectResult; // Extraindo o OkObjectResult
        Assert.NotNull(okResult);                      // Certifique-se de que não é nulo
        Assert.Equal(200, okResult.StatusCode);        // Verifique se o código de status é 200
    }

    [Fact]
    public async Task GetTodasEmpresasAsync_ShouldReturn404Status()
    {
        /// Arrange
        var empresaService = new Mock<IEmpresasService>();
        empresaService.Setup(_ => _.GetAllEmpresasService()).ReturnsAsync((List<EmpresaDTO>)null);
        var logger = new Mock<ILogger<EmpresasController>>();
        var mapper = new Mock<IMapper>();

        var sut = new EmpresasController(empresaService.Object, logger.Object, mapper.Object);

        /// Act
        var result = await sut.GetEmpresasAll();

        /// Assert
        Assert.IsType<NotFoundResult>(result.Result); // Verifique se o resultado é do tipo NotFoundResult
    }

    [Fact]
    public async Task GetEmpresaId_ShouldReturn200Status_WhenEmpresaIsFound()
    {
        // Arrange
        var empresaService = new Mock<IEmpresasService>();
        var empresaMock = new EmpresaDTO { Id = 1, Nome = "VV Alto Center", CNPJ = 189647772, Endereco = "Rua Flamengo" };
        empresaService.Setup(service => service.GetEmpresaIdService(It.IsAny<int>())).ReturnsAsync(empresaMock);

        var logger = new Mock<ILogger<EmpresasController>>();
        var mapper = new Mock<IMapper>();

        var sut = new EmpresasController(empresaService.Object, logger.Object, mapper.Object);

        // Act
        var result = await sut.GetEmpresaId(1);

        // Assert
        var okResult = result.Result as OkObjectResult; // Verifica se é um OkObjectResult
        Assert.NotNull(okResult); // Verifica se o resultado não é nulo
        Assert.Equal(200, okResult.StatusCode); // Verifica o status 200
        Assert.IsType<EmpresaDTO>(okResult.Value); // Verifica se o valor retornado é do tipo EmpresaDTO
    }

    [Fact]
    public async Task GetEmpresaId_ShouldReturn404Status_WhenEmpresaIsNotFound()
    {
        // Arrange
        var empresaService = new Mock<IEmpresasService>();
        empresaService.Setup(service => service.GetEmpresaIdService(It.IsAny<int>())).ReturnsAsync((EmpresaDTO)null);

        var logger = new Mock<ILogger<EmpresasController>>();
        var mapper = new Mock<IMapper>();

        var sut = new EmpresasController(empresaService.Object, logger.Object, mapper.Object);

        // Act
        var result = await sut.GetEmpresaId(1);

        // Assert
        var notFoundResult = result.Result as NotFoundResult; // Verifica se é um NotFoundResult
        Assert.NotNull(notFoundResult); // Verifica se o resultado não é nulo
        Assert.Equal(404, notFoundResult.StatusCode); // Verifica o status 404
    }

    [Fact]
    public async Task GetCreateEmpresa_ShouldReturn201Created_WhenEmpresaIsCreatedSuccessfully()
    {
        // Arrange
        var empresaService = new Mock<IEmpresasService>();
        var newEmpresaMock = new EmpresaDTO
        {
            Nome = "Pedro Cars",
            CNPJ = 12312344,
            Endereco = "Rua Milk Shake",
            Telefone = "9874123",
            qVagasMotos = 20,
            qVagasCarros = 2
        };

        empresaService.Setup(service => service.CreateEmpresaService(newEmpresaMock))
                      .ReturnsAsync(newEmpresaMock)
                      .Verifiable();  // Verifique se o método foi chamado corretamente

        var logger = new Mock<ILogger<EmpresasController>>();
        var mapper = new Mock<IMapper>();

        // Configure o mapper para retornar newEmpresaMock quando chamado
        mapper.Setup(m => m.Map<EmpresaDTO>(It.IsAny<Empresa>()))
              .Returns(newEmpresaMock);

        var sut = new EmpresasController(empresaService.Object, logger.Object, mapper.Object);

        // Act
        var result = await sut.PostEmpresa(newEmpresaMock);

        // Assert
        var createdResult = result.Result as CreatedAtActionResult;
        Assert.NotNull(createdResult);  // Verifica se o resultado não é nulo
        Assert.Equal(201, createdResult.StatusCode);  // Verifica se o status é 201 Created
        Assert.IsType<EmpresaDTO>(createdResult.Value);  // Verifica se o valor é do tipo EmpresaDTO
    }


    [Fact]
    public async Task GetCreateEmpresa_ShouldReturn404NotFound_WhenEmpresaIsCreatedNotFound()
    {
        // Arrange
        var empresaService = new Mock<IEmpresasService>();
        var newEmpresaMock = new EmpresaDTO
        {
            Id = 0,
            Nome = "Pedro Cars",
            CNPJ = 12312344,
            Endereco = "Rua Milk Shake",
            Telefone = "9874123",
            qVagasCarros = 20,
            qVagasMotos = 20
        };

        // Configura o mock para retornar um objeto válido (empresa criada)
        empresaService.Setup(service => service.CreateEmpresaService(newEmpresaMock))
                      .ReturnsAsync((EmpresaDTO)null);

        var logger = new Mock<ILogger<EmpresasController>>();
        var mapper = new Mock<IMapper>();

        var sut = new EmpresasController(empresaService.Object, logger.Object, mapper.Object);

        // Act
        var result = await sut.PostEmpresa(newEmpresaMock);

        // Assert
        var notFoundcreatedResult = result.Result as NotFoundResult;
        Assert.NotNull(notFoundcreatedResult); // Verifica se o resultado não é nulo
        Assert.Equal(404, notFoundcreatedResult.StatusCode); // Verifica se o status é 201 Created 
    }

    [Fact]
    public async Task GetUpdateEmpresa_ShouldReturn200Ok_WhenEmpresaIsUpdatedSuccessfully()
    {
        // Arrange
        var empresaService = new Mock<IEmpresasService>();
        var updateEmpresaMock = new EmpresaDTO
        {
            Id = 5,
            Nome = "Pedro Cars",
            CNPJ = 12312344,
            Endereco = "Rua Milk Shake",
            Telefone = "9874123",
            qVagasMotos = 20,
            qVagasCarros = 2
        };  
        
        empresaService.Setup(service => service.GetEmpresaIdService(5))
            .ReturnsAsync(new EmpresaDTO
            {
                Id = 5,
                Nome = "Pedro Cars",
                CNPJ = 12312344,
                Endereco = "Rua Milk Shake",
                Telefone = "9874123",
                qVagasMotos = 20,
                qVagasCarros = 2
            }); 
        
        empresaService.Setup(service => service.UpdateEmpresaService(5, updateEmpresaMock))
                      .ReturnsAsync(updateEmpresaMock)
                      .Verifiable();  // Verifique se o método foi chamado corretamente

        var logger = new Mock<ILogger<EmpresasController>>();
        var mapper = new Mock<IMapper>();

        var sut = new EmpresasController(empresaService.Object, logger.Object, mapper.Object);

        // Act
        var result = await sut.PutEmpresa(5, updateEmpresaMock);

        // Assert
        var updatedResult = result.Result as OkObjectResult;
        Assert.NotNull(updatedResult);  // Verifica se o resultado não é nulo
        Assert.Equal(200, updatedResult.StatusCode);  // Verifica se o status é 200 
    }

    [Fact]
    public async Task PutEmpresa_ShouldReturn400BadRequest_WhenEmpresaIsUpdatedBadRequest()
    {
        // Arrange
        var empresaService = new Mock<IEmpresasService>();
        var updateEmpresaMock = new EmpresaDTO
        {
            Id = 5,
            Nome = "Pedro Cars",
            CNPJ = 12312344,
            Endereco = "Rua Milk Shake",
            Telefone = "9874123",
            qVagasMotos = 20,
            qVagasCarros = 2
        };

        // Mock para GetEmpresaIdService
        empresaService.Setup(service => service.GetEmpresaIdService(1))
                      .ReturnsAsync(updateEmpresaMock);

        // Mock para UpdateEmpresaService
        empresaService.Setup(service => service.UpdateEmpresaService(1, updateEmpresaMock))
                      .ReturnsAsync(updateEmpresaMock)
                      .Verifiable();

        var logger = new Mock<ILogger<EmpresasController>>();
        var mapper = new Mock<IMapper>();

        var sut = new EmpresasController(empresaService.Object, logger.Object, mapper.Object);

        // Act
        var result = await sut.PutEmpresa(1, updateEmpresaMock);

        // Assert
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.NotNull(badRequestResult);
        Assert.Equal(400, badRequestResult.StatusCode);
        Assert.Equal("O ID da empresa não corresponde ao ID fornecido.", badRequestResult.Value);
    } 

    [Fact]
    public async Task GetUpdateEmpresa_ShouldReturn404NotFound_WhenEmpresaIsUpdatedNotFound()
    {
        // Arrange
        var empresaService = new Mock<IEmpresasService>();
        var updateEmpresaMock = new EmpresaDTO
        {
            Id = 5,
            Nome = "Pedro Cars",
            CNPJ = 12312344,
            Endereco = "Rua Milk Shake",
            Telefone = "9874123",
            qVagasMotos = 20,
            qVagasCarros = 2
        };

        empresaService.Setup(service => service.UpdateEmpresaService(3, updateEmpresaMock))
                      .ReturnsAsync((EmpresaDTO)null);

        var logger = new Mock<ILogger<EmpresasController>>();
        var mapper = new Mock<IMapper>();

        var sut = new EmpresasController(empresaService.Object, logger.Object, mapper.Object);

        // Act
        var result = await sut.PutEmpresa(3, updateEmpresaMock);

        // Assert
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.NotNull(notFoundResult);  // Verifica se o resultado não é nulo
        Assert.Equal(404, notFoundResult.StatusCode);  // Verifica se o status é 404 
        Assert.Equal("Não foi possível atualizar a empresa informada.", notFoundResult.Value);  // Verifica a mensagem de erro
    }

    [Fact]
    public async Task GetDeleteEmpresa_ShouldReturn200Ok_WhenEmpresaIsDeletedSuccessfully()
    {
        // Arrange
        var empresaService = new Mock<IEmpresasService>();
 
        empresaService
            .Setup(service => service.DeleteEmpresaService(5))
            .ReturnsAsync(true);  

        // Criando a empresa
        empresaService
            .Setup(service => service.GetEmpresaIdService(5))
            .ReturnsAsync(new EmpresaDTO
            {
                Id = 5,
                Nome = "Pedro Cars",
                CNPJ = 12312344,
                Endereco = "Rua Milk Shake",
                Telefone = "9874123",
                qVagasMotos = 20,
                qVagasCarros = 2
            });

        var logger = new Mock<ILogger<EmpresasController>>();
        var mapper = new Mock<IMapper>();

        var sut = new EmpresasController(empresaService.Object, logger.Object, mapper.Object);

        // Act
        var result = await sut.DeleteEmpresa(5);

        // Assert
        Assert.NotNull(result); // Certifica-se de que o resultado não é nulo
        var okDeleteEmpresa = result as OkObjectResult; // Tenta converter para OkObjectResult
        Assert.NotNull(okDeleteEmpresa); // Certifica-se de que a conversão foi bem-sucedida
        Assert.Equal(200, okDeleteEmpresa.StatusCode); // Verifica o status retornado
    }

    [Fact]
    public async Task GetDeleteEmpresa_ShouldReturn404NotFound_WhenEmpresaIsDeleteNotFound()
    {
        // Arrange
        var empresaService = new Mock<IEmpresasService>();
        var updateEmpresaMock = new EmpresaDTO
        {
            Id = 5,
            Nome = "Pedro Cars",
            CNPJ = 12312344,
            Endereco = "Rua Milk Shake",
            Telefone = "9874123",
            qVagasMotos = 20,
            qVagasCarros = 2
        };

        empresaService.Setup(service => service.DeleteEmpresaService(3));

        var logger = new Mock<ILogger<EmpresasController>>();
        var mapper = new Mock<IMapper>();

        var sut = new EmpresasController(empresaService.Object, logger.Object, mapper.Object);

        // Act
        var result = await sut.DeleteEmpresa(3);

        // Assert
        var okDeleteEmpresaNotFound = result as OkObjectResult;
        Assert.NotNull(okDeleteEmpresaNotFound);  // Verifica se o resultado não é nulo
        Assert.Equal(500, okDeleteEmpresaNotFound.StatusCode);  // Verifica se o status é 500  
        Assert.Equal("Empresa não encontrada no banco de dados.", okDeleteEmpresaNotFound.Value);  // Verifica se o status é 200  
    }
}