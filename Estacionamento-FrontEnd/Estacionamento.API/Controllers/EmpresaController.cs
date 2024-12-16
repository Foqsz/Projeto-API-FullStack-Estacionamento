using Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface;
using Estacionamento_FrontEnd.Estacionamento.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento_FrontEnd.Estacionamento.API.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly IEmpresaService _empresaService;

        public EmpresaController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaViewModel>>> Index()
        {
            var empresas = await _empresaService.GetEmpresaAll();

            return empresas is null ? View("Error") : View(empresas);
        }


    }
}
