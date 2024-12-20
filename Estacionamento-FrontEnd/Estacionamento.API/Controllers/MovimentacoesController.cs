using Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface;
using Estacionamento_FrontEnd.Estacionamento.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Numerics;

namespace Estacionamento_FrontEnd.Estacionamento.API.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly IMovimentacoesService _movimentacoesService;

        public MovimentacoesController(IMovimentacoesService movimentacoesService)
        {
            _movimentacoesService = movimentacoesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimentacaoViewModel>>> Index()
        {
            var estacionadosAll = await _movimentacoesService.GetEstacionadosAll();

            return estacionadosAll is null ? View("Index") : View(estacionadosAll);
        }

        [HttpGet]
        public async Task<ActionResult> RegistrarEntrada()
        {
            ViewBag.Id = new SelectList(await _movimentacoesService.GetEstacionadosAll());

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarEntrada(string placaVeiculo, string tipoVeiculo)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");
            await _movimentacoesService.RegistrarEntrada(placaVeiculo, tipoVeiculo);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> RegistrarSaida(int id, string placa)
        {
            var veiculoEstacionamento = new MovimentacaoViewModel();

            veiculoEstacionamento.Id = id;
            veiculoEstacionamento.PlacaVeiculo = placa;

            return View(veiculoEstacionamento);
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarSaida(MovimentacaoViewModel veiculo)
        {
            await _movimentacoesService.RegistrarSaida(veiculo.Id, veiculo.PlacaVeiculo);

            return RedirectToAction("Index", "Movimentacoes");
        }
    }
}
