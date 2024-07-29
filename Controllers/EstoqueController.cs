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
using System.Data;

namespace TOLEAGRI.Controllers
{
    public class EstoqueController : Controller
    {
        //private readonly TOLEDbContext _dbContext;
        private readonly EstoqueService estoqueService;

        private readonly TOLEDbContext dbContext;
        public EstoqueController(EstoqueService estoqueService, TOLEDbContext dbContext)
        {
            this.estoqueService = estoqueService;
            this.dbContext = dbContext;
        }

        /// Faz a abertura da view estoque para cliente
        public IActionResult EstoqueIndex()
        {
            var model = estoqueService.GetAll();
            return View(model);
        }

        // Faz a abertura do Modal que da saida a uma peça
        [HttpGet]

        public IActionResult ModalSaidaEstoque()
        {
            return View("Modal/SaidaEstoque", new Peca());
        }

        // Método que busca o serviço que mostra as informações da peça, se não houver informações cria uma peça nova
        [HttpPost]

        public IActionResult SaidaEstoque(Peca peca)
        {


            if (!dbContext.Pecas.Any(e => e.CodigoSistema == peca.CodigoSistema))
            {
                ModelState.AddModelError("CodigoSistema", "Código do Sistema não encontrado.");
            }

            if (!ModelState.IsValid) 
            { 
                
                return View("Modal/SaidaEstoque", peca);
            }
            
                estoqueService.BuscarModificarSaida(peca);
                return Json(new { success = true });
        }


        // FAZ A ABERTURA DO MODAL QUE DA ENTRADA A UMA PEÇA
        [HttpGet]

        public IActionResult ModalEntradaEstoque()
        {
            return View("Modal/EntradaEstoque", new Peca());
        }



        // Método que busca o serviço que mostra as informações da peça, se não houver informações cria uma peça nova
        [HttpPost]

        public IActionResult EntradaEstoque(Peca peca)
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
        public IActionResult ModalDeletarPeca(int id)
        {
            Peca peca = estoqueService.Get(id);
            return View("Modal/DeletarPeca");
        }
        


        // Método que deleta uma peça
        [HttpPost]
        public IActionResult DeletarPeca(int id)
        {
            estoqueService.Delete(id);
            return RedirectToAction("EstoqueIndex");
        }



        //Abertura do Modal que deleta todas as peças
       [HttpGet]
        public IActionResult ModalDeleteAllPeca()
        {
            return View("Modal/DeleteAllPeca");
        }



       //Método que deleta todas as peças
       [HttpPost]
        public IActionResult DeleteAllPeca()
        {
            estoqueService.DeleteAll();
            return RedirectToAction("EstoqueIndex");
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


