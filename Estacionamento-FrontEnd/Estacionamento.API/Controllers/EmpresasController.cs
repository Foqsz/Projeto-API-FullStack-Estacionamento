﻿using Estacionamento_FrontEnd.Estacionamento.Application.Service.Interface;
using Estacionamento_FrontEnd.Estacionamento.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Estacionamento_FrontEnd.Estacionamento.API.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly IEmpresaService _empresaService;

        public EmpresasController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaViewModel>>> Index()
        {
            var empresas = await _empresaService.GetEmpresaAll();

            return empresas is null ? View("Error") : View(empresas);
        }

        [HttpGet]
        public async Task<ActionResult> UpdateEmpresa(int id)
        {
            var empresa = await _empresaService.GetEmpresaById(id);

            if (empresa is null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateEmpresa(int id, EmpresaViewModel empresa)
        {
            if (!ModelState.IsValid) return View(empresa);

            await _empresaService.GetEmpresaById(id);

            if (empresa is null) return View(empresa);
            await _empresaService.PutEmpresa(id, empresa);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> CreateEmpresa()
        {
            ViewBag.Id = new SelectList(await _empresaService.GetEmpresaAll());

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmpresa(EmpresaViewModel empresa)
        {
            if (!ModelState.IsValid) return View(empresa);
            await _empresaService.PostEmpresa(empresa);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> DeletarEmpresa(int id)
        {
            var empresa = await _empresaService.GetEmpresaById(id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        [HttpPost]
        public async Task<ActionResult> DeletarEmpresa(EmpresaViewModel empresa)
        {
            if (!ModelState.IsValid)
            {
                return View(empresa);
            }

            if (empresa?.Id == null)
            {
                return RedirectToAction("Index");
            }

            await _empresaService.DeletarEmpresa(empresa.Id);

            return RedirectToAction("Index", "Empresas");
        }
    }
}
