using Microsoft.AspNetCore.Mvc;
using TOLEAGRI.Model.Services;
using TOLEAGRI.Model.Domain;
using TOLEAGRI.Model.Persistence;
using TOLEAGRI.Model;
using System.Diagnostics;

namespace TOLEAGRI.Controllers
{
    public class EstoqueController : Controller
    {
        private readonly EstoqueService estoqueService;

        public EstoqueController(EstoqueService estoqueService)
        {
            this.estoqueService = estoqueService;
        }

        public IActionResult Index()
        {
            var model = estoqueService.GetAll();
            return View(model);
        }

        [HttpGet]
        
        public IActionResult ModalEntradaEstoque()
        {
            return PartialView("Modal/EntradaEstoque", new Estoque());
        }

        [HttpPost]

        public IActionResult EntradaEstoque(string codigoSistema)
        {
            estoqueService.BuscarOuCriar(codigoSistema);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ModalSaidaEstoque(string codigoSistema)
        {
            Estoque estoque = estoqueService.BuscarOuCriar(codigoSistema);
            return View("Modal/SaidaEstoque", estoque);
        }

        [HttpPost]
        public IActionResult SaidaEstoque(Estoque estoque)
        {
            estoqueService.Update(estoque);
            return RedirectToAction("Index");
        }
    }
}







//[HttpGet]
//public IActionResult ModalEntradaEstoque(string codigoSistema)
//{
//    Estoque estoque = estoqueService.GetByCodigoSistema(codigoSistema);
//    return View("Modal/EntradaEstoque", estoque);
//}

//[HttpPost]
//public IActionResult EntradaEstoque(Estoque estoque)
//{
//    estoqueService.Update(estoque);
//    return RedirectToAction("Index");
//}