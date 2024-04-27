using Microsoft.AspNetCore.Mvc;
using TOLEAGRI.Model.Services;
using TOLEAGRI.Model;
using TOLEAGRI.Model.Domain;
using TOLEAGRI.Model.Persistence;
using System.Diagnostics;

namespace TOLEAGRI.Model.Controllers
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
    }
}
