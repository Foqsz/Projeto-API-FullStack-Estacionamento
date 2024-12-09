using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.API.Controllers;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.DTOs;
using Projeto_API_BackEnd_Estacionamento.Estacionamento.Application.Interfaces;
using Testes_API_BackEnd.MockData;

namespace Testes_API_BackEnd.Controllers;

public class MovimentacaoControllerTests
{
    [Fact]
    public async Task GetMovimentacioesAll_Return200StatusOk()
    {
        /// Arrange
        var movimentacaoService = new Mock<IMovimentacaoService>();
        movimentacaoService.Setup(_ => _.GetAllEstacionados()).ReturnsAsync(MovimentacoesMockData.GetMovimentacoesNull());

        var logger = new Mock<ILogger<MovimentacaoController>>();
        var mapper = new Mock<IMapper>();

        var sut = new MovimentacaoController(movimentacaoService.Object, logger.Object, mapper.Object);

        //Act
        var result = await sut.VeiculosEstacionados();

        //Assert
        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetMovimentacioesAll_Return404NotFound()
    {
        /// Arrange
        var movimentacaoService = new Mock<IMovimentacaoService>();
        movimentacaoService.Setup(_ => _.GetAllEstacionados()).ReturnsAsync((List<MovimentacaoEstacionamentoDTO>)null);

        var logger = new Mock<ILogger<MovimentacaoController>>();
        var mapper = new Mock<IMapper>();

        var sut = new MovimentacaoController(movimentacaoService.Object, logger.Object, mapper.Object);

        //Act
        var result = await sut.VeiculosEstacionados();

        //Assert
        var okResultNotFound = result.Result as NotFoundResult;
        Assert.NotNull(okResultNotFound);
        Assert.Equal(404, okResultNotFound.StatusCode);
    }

    [Fact]
    public async Task PostRegistrarEntradaMovientacao_Return200StatusOk()
    {
        /// Arrange
        var movimentacaoService = new Mock<IMovimentacaoService>();
        var newMovientacao = new MovimentacaoEstacionamentoDTO
        {
            Id = 1,
            PlacaVeiculo = "12AS32",
            HoraEntrada = DateTime.Now,
            HoraSaida = DateTime.Now.AddMinutes(10),
            TipoVeiculo = "SEDAN"

        };

        movimentacaoService.Setup(service => service.RegistrarEntrada("12AS32", "SEDAN"));

        var logger = new Mock<ILogger<MovimentacaoController>>();
        var mapper = new Mock<IMapper>();

        var sut = new MovimentacaoController(movimentacaoService.Object, logger.Object, mapper.Object);

        //Act
        var result = await sut.RegistrarEntrada("12AS32", "SEDAN");

        //Assert
        var okResultNotFound = result as OkObjectResult;
        Assert.NotNull(okResultNotFound);
        Assert.Equal(200, okResultNotFound.StatusCode);
    }

    [Fact]
    public async Task PostRegistrarEntradaMovientacao_Return400NotFound()
    {
        /// Arrange
        var movimentacaoService = new Mock<IMovimentacaoService>();
        var newMovientacao = new MovimentacaoEstacionamentoDTO
        {
            Id = 1,
            PlacaVeiculo = "12AS32",
            HoraEntrada = DateTime.Now,
            HoraSaida = DateTime.Now.AddMinutes(10),
            TipoVeiculo = "SEDAN"

        };

        movimentacaoService.Setup(service => service.RegistrarEntrada("12AS32", "SEDAN"));

        var logger = new Mock<ILogger<MovimentacaoController>>();
        var mapper = new Mock<IMapper>();

        var sut = new MovimentacaoController(movimentacaoService.Object, logger.Object, mapper.Object);

        //Act
        var result = await sut.RegistrarEntrada("45A7SS", "SEDAN");

        //Assert
        var okResultNotFound = result as NotFoundResult;
        Assert.NotNull(okResultNotFound);
        Assert.Equal(404, okResultNotFound.StatusCode);
    }

    [Fact]
    public async Task PostRegistrarSaidaMovientacao_Return200StatusOk()
    {
        /// Arrange
        var movimentacaoService = new Mock<IMovimentacaoService>();
        var newMovientacao = new MovimentacaoEstacionamentoDTO
        {
            Id = 1,
            PlacaVeiculo = "12AS32",
            HoraEntrada = DateTime.Now,
            HoraSaida = DateTime.Now.AddMinutes(10),
            TipoVeiculo = "SEDAN"

        };

        movimentacaoService.Setup(service => service.RegistrarSaida(1, "12AS32")).ReturnsAsync(newMovientacao).Verifiable();

        var logger = new Mock<ILogger<MovimentacaoController>>();
        var mapper = new Mock<IMapper>();

        var sut = new MovimentacaoController(movimentacaoService.Object, logger.Object, mapper.Object);

        //Act
        var result = await sut.RegistrarSaida(1, "12AS32");

        //Assert
        var okResultNotFound = result.Result as OkObjectResult;
        Assert.NotNull(okResultNotFound);
        Assert.Equal(200, okResultNotFound.StatusCode);
    }
}
