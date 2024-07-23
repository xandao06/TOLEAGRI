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
        public IActionResult RegistroIndex()
        {
            var registros = registroService.GetAll();
            return View(registros);
        }


        // Abertura do Modal para deletar um registro
        [HttpGet]
        public IActionResult ModalDeletarRegistro(int id)
        {
            RegistroPeca registroPeca = registroService.Get(id);
            return View("Modal/DeletarRegistro");
        }

        // Método que deleta um registro
        [HttpPost]
        public IActionResult DeletarRegistro(int id)
        {
            registroService.Delete(id);
            return RedirectToAction("RegistroIndex");
        }

        //Abertura do Modal que deleta todos os registros
       [HttpGet]
        public IActionResult ModalDeleteAllRegistro()
        {
            return View("Modal/DeleteAllRegistro");
        }

        //Método que deleta todos os registros
       [HttpPost]
        public IActionResult DeleteAllRegistro()
        {
            registroService.DeleteAll();
            return RedirectToAction("RegistroIndex");
        }

        // Método que busca o serviço de filtragem de registros
        [HttpGet]
        public IActionResult SearchReg(string query, DateTime? startDate, DateTime? endDate)
        {
            var registros = registroService.SearchRegistros(query, startDate, endDate);
            return Json(registros);
        }
    }
}