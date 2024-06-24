using Microsoft.AspNetCore.Mvc;
using TOLEAGRI.Model.Services;
using TOLEAGRI.Model.Domain;
using TOLEAGRI.Model.Persistence;
using TOLEAGRI.Model;
using System.Diagnostics;

namespace TOLEAGRI.Controllers
{
    public class RegistroController : Controller
    {
        private readonly RegistroService registroService;

        public RegistroController(RegistroService registroService)
        {
            this.registroService = registroService;
        }

        // Faz a filtragem de registros na barra de pesquisa
        public IActionResult Registro()
        {
            var registros = registroService.GetAll();
            return View(registros);
        }

        [HttpGet]
        public IActionResult ModalDeletarRegistro(int id)
        {
            RegistroPeca registroPeca = registroService.Get(id);
            return View("Modal/DeletarRegistro");
        }

        [HttpPost]
        public IActionResult DeletarRegistro(int id)
        {
            registroService.Delete(id);
            return RedirectToAction("Registro");
        }

        [HttpGet]
        public IActionResult ModalDeletarAllRegistro()
        {
            return View("Modal/DeletarAllRegistro");
        }

        [HttpPost]
        public IActionResult DeletarAllRegistro()
        {
            registroService.DeleteAll();
            return RedirectToAction("Registro");
        }
    }
}