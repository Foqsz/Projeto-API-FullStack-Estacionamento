using Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface;
using Estacionamento_FrontEnd.Estacionamento.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Estacionamento_FrontEnd.Estacionamento.API.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly IVeiculoService _veiculoService;

        public VeiculosController(IVeiculoService veiculoService)
        {
            _veiculoService = veiculoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculosViewModel>>> Index()
        {
            var veiculos = await _veiculoService.GetVeiculosAll();

            return veiculos is null ? View("Error") : View(veiculos);
        }

        [HttpGet]
        public async Task<ActionResult> CreateVeiculo()
        {
            ViewBag.Id = new SelectList(await _veiculoService.GetVeiculosAll());

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateVeiculo(VeiculosViewModel veiculo)
        {
            if (!ModelState.IsValid) return View(veiculo);
            await _veiculoService.PostVeiculo(veiculo);
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<ActionResult> UpdateVeiculo(int id)
        {
            var veiculoId = await _veiculoService.GetVeiculoById(id);
            return veiculoId is null ? View("Error") : View(veiculoId);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateVeiculo(int id, VeiculosViewModel veiculo)
        {
            if (id != veiculo.Id)
            {
                return View("Error");
            }

            await _veiculoService.PutVeiculo(id, veiculo);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> DeletarVeiculo(int id)
        {
            var deletarVeiculo = await _veiculoService.GetVeiculoById(id);
            return deletarVeiculo == null ? View("Error") : View(deletarVeiculo);
        }

        [HttpPost]
        public async Task<ActionResult> DeletarVeiculo(VeiculosViewModel veiculo)
        {
            if (!ModelState.IsValid)
            {
                return View(veiculo);
            }

            await _veiculoService.DeleteVeiculo(veiculo.Id);

            return RedirectToAction("Index", "Veiculos");
        }
    }
}
