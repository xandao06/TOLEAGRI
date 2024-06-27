using Microsoft.AspNetCore.Mvc;
using TOLEAGRI.Model.Services;
using TOLEAGRI.Model.Domain;
using TOLEAGRI.Model.Persistence;
using TOLEAGRI.Model;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Azure.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.ApplicationModels;


namespace TOLEAGRI.Controllers
{
    public class ManagerController : Controller
    {  

        private readonly EstoqueService estoqueService;
        private readonly RegistroService registroService;

        public ManagerController(EstoqueService estoqueService, RegistroService registroService)
        {
            this.estoqueService = estoqueService;
            this.registroService = registroService;
        }

        // Faz a abertura da view estoque para manager
        public IActionResult EstoqueManager()
        {
            var model = estoqueService.GetAll();
            return View(model);
        }


        // Faz a abertura do Modal que cria uma peça
        [HttpGet]

        public IActionResult ModalLoginManager()
        {
            return View("LoginManager");
        }


        // Faz a abertura da view estoque para manager
        public IActionResult RegistroManager()
        {
            var model = registroService.GetAll();
            return View(model);
        }
    }
}
