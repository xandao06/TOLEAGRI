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

        // Faz a abertura da View Registro buscando o serviço que cria um registro de cada Peca adicionada ou modificada
        public IActionResult Registro()
        {
            var model = registroService.RegistroList();
            return View(model);
        }
    }
}