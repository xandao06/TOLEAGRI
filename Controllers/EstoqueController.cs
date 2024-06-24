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

        // Faz a abertura da view e puxa uma lista das Pecas
        public IActionResult Index()
        {
            var model = estoqueService.GetAll();
            return View(model);
        }

        // Faz a filtragem das pecas na barra de pesquisa da Index
        //[HttpGet("search")]
        //public IActionResult Search([FromQuery] string query)
        //{
        //    var result = estoqueService.SearchPecas(query);
        //    return Ok(result);
        //}

        // Faz a abertura do Modal com Form para criação e atualização das Pecas 
        [HttpGet]

        public IActionResult ModalEntradaSaidaEstoque()
        {
            return View("Modal/EntradaSaidaEstoque", new Peca());
        }

        // Busca o serviço que faz a busca da Peca pelo codigo decide se vai criar ou modificar e redireciona para a View Index
        [HttpPost]

        public IActionResult EntradaSaidaEstoque(Peca peca)
        {
            estoqueService.BuscarModificarCriar(peca);
            return RedirectToAction("Index");
        }
        
        // Busca as informações da Peca pelo codigo do sistema
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

        // Faz a abertura do Modal que deleta a Peca
        [HttpGet]
        public IActionResult ModalDeletarEstoque(int id)
        {
            Peca peca = estoqueService.Get(id);
            return View("Modal/DeletarEstoque");
        }
        
        // Busca o serviço que faz o processo de buscar a Peca pelo Id e deletar ela do banco e depois retorna para a View Index
        [HttpPost]
        public IActionResult DeletarEstoque(int id)
        {
            estoqueService.Delete(id);
            return RedirectToAction("Index");
        }

        // Abertura do Modal que deleta tudo
        [HttpGet]
        public IActionResult ModalDeletarAllEstoque()
        {
            return View("Modal/DeletarAllEstoque");
        }

        // Busca o serviço que deleta tudo
        [HttpPost]
        public IActionResult DeletarAllEstoque()
        {
            estoqueService.DeleteAll();
            return RedirectToAction("Index");
        }

    }
}
