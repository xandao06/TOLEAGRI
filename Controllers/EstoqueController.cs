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
            return View("Modal/EntradaEstoque", new Peca());
        }

        [HttpPost]

        public IActionResult EntradaEstoque(Peca peca)
        {
            estoqueService.Add(peca);
            return RedirectToAction("Index");
        }

        [HttpGet]

        public IActionResult ModalSaidaEstoque(Peca peca)
        {
            return View("Modal/SaidaEstoque", peca);
        } 

        [HttpPost]

        public IActionResult SaidaEstoque(Peca peca)
        {
            estoqueService.BuscarModificar(peca);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetByCodigoSistema(string codigoSistema)
        {
            Peca peca = estoqueService.GetByCodigoSistema(codigoSistema);

            if (peca != null)
            {
                return Json(new { locacao = peca.Locacao, marca = peca.Marca, modelo = peca.Modelo, quantidade = peca.Quantidade, notaoupedido = peca.NotaOuPedido, observacao = peca.Observacao });
            }

            return Json(null);
        }

        [HttpGet]
        public IActionResult ModalDeletarEstoque(int id)
        {
            Peca peca = estoqueService.Get(id);
            return View("Modal/DeletarEstoque");
        }

        [HttpPost]
        public IActionResult DeletarEstoque(int id)
        {
            estoqueService.Delete(id);
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