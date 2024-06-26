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
    public class EstoqueController : Controller
    {
        //private readonly TOLEDbContext _dbContext;
        private readonly EstoqueService estoqueService;

        public EstoqueController(EstoqueService estoqueService)
        {
            this.estoqueService = estoqueService;
        }

        /// Faz a abertura da view estoque para cliente
        public IActionResult EstoqueIndex()
        {
            var model = estoqueService.GetAll();
            return View(model);
        }

        // Faz a abertura da view estoque para manager
        public IActionResult EstoqueManage()
        {
            var model = estoqueService.GetAll();
            return View(model);
        }

        // Faz a abertura do Modal que cria uma peça
        [HttpGet]

        public IActionResult ModalEntradaSaidaEstoque()
        {
            return View("Modal/EntradaSaidaEstoque", new Peca());
        }

        // Método que busca o serviço que mostra as informações da peça, se não houver informações cria uma peça nova
        [HttpPost]

        public IActionResult EntradaSaidaEstoque(Peca peca)
        {
            estoqueService.BuscarModificarCriar(peca);
            return RedirectToAction("EstoqueIndex");
        }
        
        // Método que busca as informações da peça pelo atributo "CodigoSistema"
        [HttpGet]
        public IActionResult GetByCodigoSistema(string codigoSistema)
        {
            Peca peca = estoqueService.GetByCodigoSistema(codigoSistema);

            if (peca != null)
            {
                return Json(new { locacao = peca.Locacao, marca = peca.Marca, modelo = peca.Modelo, quantidade = peca.Quantidade, notaoupedido = peca.NotaOuPedido});
            }

            return Json(null);
        }

        // Faz a abertura do Modal que deleta uma peça
        [HttpGet]
        public IActionResult ModalDeletarEstoque(int id)
        {
            Peca peca = estoqueService.Get(id);
            return View("Modal/DeletarEstoque");
        }
        
        // Método que deleta uma peça
        [HttpPost]
        public IActionResult DeletarPeca(int id)
        {
            estoqueService.Delete(id);
            return RedirectToAction("EstoqueManage");
        }

        // Abertura do Modal que deleta todas as peças
        [HttpGet]
        public IActionResult ModalDeletarAllPeca()
        {
            return View("Modal/DeletarAllPeca");
        }

        // Método que deleta todas as peças
        [HttpPost]
        public IActionResult DeletarAllPeca()
        {
            estoqueService.DeleteAll();
            return RedirectToAction("EstoqueManage");
        }

        // Método que busca o serviço de filtragem de peças
        [HttpGet]
        public IActionResult SearchPec(string query, DateTime? startDate, DateTime? endDate)
        {
            var pecas = estoqueService.SearchPecas(query, startDate, endDate);
            return Json(pecas);
        }
    }
}
