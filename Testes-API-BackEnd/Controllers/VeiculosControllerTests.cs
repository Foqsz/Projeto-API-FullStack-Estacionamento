using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.API.Controllers;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Testes_API_BackEnd.MockData;

namespace Testes_API_BackEnd.Controllers;

public class VeiculosControllerTests
{
    [Fact]
    public async Task GetTodosVeiculos_ShouldReturn200Status()
    {
        //Arrange
        var veiculoService = new Mock<IVeiculosService>();
        veiculoService.Setup(_ => _.GetAllVeiculos()).ReturnsAsync(VeiculosMockData.GetVeiculos());

        var logger = new Mock<ILogger<VeiculosController>>();
        var mapper = new Mock<IMapper>();

        var sut = new VeiculosController(veiculoService.Object, mapper.Object, logger.Object);

        //Act
        var result = await sut.GetVeiculosAll();

        //Assert
        var okResultVeiculos = result.Result as OkObjectResult;
        Assert.NotNull(okResultVeiculos);
        Assert.Equal(200, okResultVeiculos.StatusCode);
    }

    [Fact]
    public async Task GetTodosVeiculos_ShouldReturn404NotFound()
    {
        //Arrange
        var veiculoService = new Mock<IVeiculosService>();
        veiculoService.Setup(_ => _.GetAllVeiculos()).ReturnsAsync((List<VeiculosDTO>)null);

        var logger = new Mock<ILogger<VeiculosController>>();
        var mapper = new Mock<IMapper>();

        var sut = new VeiculosController(veiculoService.Object, mapper.Object, logger.Object);

        //Act
        var result = await sut.GetVeiculosAll();

        //Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetVeiculoId_ShouldReturn200Status()
    {
        //Arrange
        var veiculoService = new Mock<IVeiculosService>();
        var veiculoMock = new VeiculosDTO { Id = 1, Marca = "Toyota", Modelo = "Corolla", Cor = "Preto", Placa = "B23XT5G1", Tipo = "Sedan" };
        veiculoService.Setup(service => service.GetVeiculoId(It.IsAny<int>())).ReturnsAsync(veiculoMock);

        var logger = new Mock<ILogger<VeiculosController>>();
        var mapper = new Mock<IMapper>();

        var sut = new VeiculosController(veiculoService.Object, mapper.Object, logger.Object);

        //Act
        var result = await sut.GetVeiculoById(1);

        //Assert
        var okResultVeiculos = result.Result as OkObjectResult;
        Assert.NotNull(okResultVeiculos);
        Assert.Equal(200, okResultVeiculos.StatusCode);
        Assert.IsType<VeiculosDTO>(okResultVeiculos.Value);
    }

    [Fact]
    public async Task GetVeiculoId_ShouldReturn404NotFound()
    {
        //Arrange
        var veiculoService = new Mock<IVeiculosService>();
        veiculoService.Setup(service => service.GetVeiculoId(It.IsAny<int>())).ReturnsAsync((VeiculosDTO)null);

        var logger = new Mock<ILogger<VeiculosController>>();
        var mapper = new Mock<IMapper>();

        var sut = new VeiculosController(veiculoService.Object, mapper.Object, logger.Object);

        //Act
        var result = await sut.GetVeiculoById(1);

        //Assert
        var okResultVeiculos = result.Result as NotFoundResult;
        Assert.NotNull(okResultVeiculos);
        Assert.Equal(404, okResultVeiculos.StatusCode);
    }

    [Fact]
    public async Task GetCreateVeiculo_ShouldReturn200Status()
    {
        //Arrange
        var veiculoService = new Mock<IVeiculosService>();
        var newVeiculoMock = new VeiculosDTO
        {
            Id = 1,
            Marca = "Chevrolet",
            Modelo = "Onix",
            Cor = "Azul",
            Placa = "C89YU1H",
            Tipo = "Hatchback"
        };

        veiculoService.Setup(service => service.CreateVeiculo(newVeiculoMock)).ReturnsAsync(newVeiculoMock).Verifiable(); //verifiable para verificar se o método foi chamado corretamente


        var logger = new Mock<ILogger<VeiculosController>>();
        var mapper = new Mock<IMapper>();

        var sut = new VeiculosController(veiculoService.Object, mapper.Object, logger.Object);

        //Act
        var result = await sut.PostVeiculo(newVeiculoMock);

        //Assert
        var createVeiculoResult = result.Result as CreatedAtActionResult;
        Assert.NotNull(createVeiculoResult);
        Assert.Equal(201, createVeiculoResult.StatusCode);
        Assert.IsType<VeiculosDTO>(createVeiculoResult.Value);
    }

    [Fact]
    public async Task GetCreateVeiculo_ShouldReturn404NotFound()
    {
        //Arrange
        var veiculoService = new Mock<IVeiculosService>();
        var newVeiculoMock = new VeiculosDTO
        {
            Id = 1,
            Marca = "Chevrolet",
            Modelo = "Onix",
            Cor = "Azul",
            Placa = "C89YU1H",
            Tipo = "Hatchback"
        };

        veiculoService.Setup(service => service.CreateVeiculo(newVeiculoMock)).ReturnsAsync((VeiculosDTO)null);


        var logger = new Mock<ILogger<VeiculosController>>();
        var mapper = new Mock<IMapper>();

        var sut = new VeiculosController(veiculoService.Object, mapper.Object, logger.Object);

        //Act
        var result = await sut.PostVeiculo(newVeiculoMock);

        //Assert
        var createVeiculoResultNoTfound = result.Result as NotFoundResult;
        Assert.NotNull(createVeiculoResultNoTfound);
        Assert.Equal(404, createVeiculoResultNoTfound.StatusCode);
    }

    [Fact]
    public async Task PutVeiculo_ShouldReturn200Status()
    {
        // Arrange
        var veiculoService = new Mock<IVeiculosService>();
        var updateVeiculoMock = new VeiculosDTO
        {
            Id = 1,
            Marca = "Chevrolet",
            Modelo = "Onix",
            Cor = "Azul",
            Placa = "C89YU1H",
            Tipo = "Hatchback"
        };

        // Configuração do Mock
        veiculoService.Setup(service => service.GetVeiculoId(1)).ReturnsAsync(updateVeiculoMock);
        veiculoService.Setup(service => service.UpdateVeiculo(1, updateVeiculoMock)).ReturnsAsync(updateVeiculoMock).Verifiable();

        var logger = new Mock<ILogger<VeiculosController>>();
        var mapper = new Mock<IMapper>();

        // Configurar o Mock do mapper se necessário
        mapper.Setup(m => m.Map<VeiculosDTO>(It.IsAny<VeiculosDTO>())).Returns(updateVeiculoMock);

        var sut = new VeiculosController(veiculoService.Object, mapper.Object, logger.Object);

        // Act
        var result = await sut.PutVeiculo(1, updateVeiculoMock);

        // Assert
        var putVeiculo = result.Result as OkObjectResult;
        Assert.NotNull(putVeiculo);
        Assert.Equal(200, putVeiculo.StatusCode);
        Assert.IsType<VeiculosDTO>(putVeiculo.Value);
    }

    [Fact]
    public async Task PutVeiculo_ShouldReturn400BadRequest()
    {
        // Arrange
        var veiculoService = new Mock<IVeiculosService>();
        var updateVeiculoMock = new VeiculosDTO
        {
            Id = 1,
            Marca = "Chevrolet",
            Modelo = "Onix",
            Cor = "Azul",
            Placa = "C89YU1H",
            Tipo = "Hatchback"
        };

        // Configuração do Mock
        veiculoService.Setup(service => service.GetVeiculoId(It.IsAny<int>())).ReturnsAsync(updateVeiculoMock);

        var logger = new Mock<ILogger<VeiculosController>>();
        var mapper = new Mock<IMapper>();

        // Configurar o Mock do mapper se necessário
        mapper.Setup(m => m.Map<VeiculosDTO>(It.IsAny<VeiculosDTO>())).Returns(updateVeiculoMock);

        var sut = new VeiculosController(veiculoService.Object, mapper.Object, logger.Object);

        // Act
        var result = await sut.PutVeiculo(2, updateVeiculoMock);

        // Assert
        var putVeiculoBadRequest = result.Result as BadRequestObjectResult;
        Assert.NotNull(putVeiculoBadRequest);
        Assert.Equal(400, putVeiculoBadRequest.StatusCode);
        Assert.Equal("O ID do veiculo não corresponde ao ID fornecido.", putVeiculoBadRequest.Value);  
    }

    [Fact]
    public async Task PutVeiculo_ShouldReturn404NotFound()
    {
        // Arrange
        var veiculoService = new Mock<IVeiculosService>();
        var updateVeiculoMock = new VeiculosDTO
        {
            Id = 1,
            Marca = "Chevrolet",
            Modelo = "Onix",
            Cor = "Azul",
            Placa = "C89YU1H",
            Tipo = "Hatchback"
        };

        // Configuração do Mock
        veiculoService.Setup(service => service.UpdateVeiculo(3, updateVeiculoMock)).ReturnsAsync((VeiculosDTO)null);

        var logger = new Mock<ILogger<VeiculosController>>();
        var mapper = new Mock<IMapper>();

        // Configurar o Mock do mapper se necessário
        mapper.Setup(m => m.Map<VeiculosDTO>(It.IsAny<VeiculosDTO>())).Returns(updateVeiculoMock);

        var sut = new VeiculosController(veiculoService.Object, mapper.Object, logger.Object);

        // Act
        var result = await sut.PutVeiculo(3, updateVeiculoMock);

        // Assert
        var putVeiculoNotFound = result.Result as NotFoundObjectResult;
        Assert.NotNull(putVeiculoNotFound);
        Assert.Equal(404, putVeiculoNotFound.StatusCode);
        Assert.Equal("Não foi possível atualizar o veiculo.", putVeiculoNotFound.Value);
    }

    [Fact]
    public async Task DeleteVeiculo_ShouldReturn200StatusOk()
    {
        // Arrange
        var veiculoService = new Mock<IVeiculosService>();
        var newVeiculoMock = new VeiculosDTO
        {
            Id = 1,
            Marca = "Chevrolet",
            Modelo = "Onix",
            Cor = "Azul",
            Placa = "C89YU1H",
            Tipo = "Hatchback"
        };

        // Configuração do Mock
        veiculoService.Setup(service => service.DeleteVeiculo(1));

        var logger = new Mock<ILogger<VeiculosController>>();
        var mapper = new Mock<IMapper>(); 

        var sut = new VeiculosController(veiculoService.Object, mapper.Object, logger.Object);

        // Act
        var result = await sut.DeleteVeiculo(1);

        // Assert
        var deleteVeiculo = result as OkObjectResult;
        Assert.NotNull(deleteVeiculo);
        Assert.Equal(200, deleteVeiculo.StatusCode); 
    }

    [Fact]
    public async Task DeleteVeiculo_ShouldReturn404NotFound()
    {
        // Arrange
        var veiculoService = new Mock<IVeiculosService>();
        var newVeiculoMock = new VeiculosDTO
        {
            Id = 1,
            Marca = "Chevrolet",
            Modelo = "Onix",
            Cor = "Azul",
            Placa = "C89YU1H",
            Tipo = "Hatchback"
        };

        // Configuração do Mock
        veiculoService.Setup(service => service.DeleteVeiculo(2));

        var logger = new Mock<ILogger<VeiculosController>>();
        var mapper = new Mock<IMapper>();

        var sut = new VeiculosController(veiculoService.Object, mapper.Object, logger.Object);

        // Act
        var result = await sut.DeleteVeiculo(2);

        // Assert
        var deleteVeiculoNotFound = result as NotFoundResult;
        Assert.NotNull(deleteVeiculoNotFound);
        Assert.Equal(404, deleteVeiculoNotFound.StatusCode);
    }
}
