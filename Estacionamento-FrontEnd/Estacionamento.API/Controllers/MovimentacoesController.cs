using Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface;
using Estacionamento_FrontEnd.Estacionamento.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
