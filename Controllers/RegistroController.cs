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
            var registros = registroService.GetRegistros();
            return View(registros);
        }
    }
}