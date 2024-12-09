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
}
