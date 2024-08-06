using Microsoft.AspNetCore.Mvc;
using TOLEAGRI.Model.Services;
using TOLEAGRI.Model.Domain;
using TOLEAGRI.Model.Persistence;
using TOLEAGRI.Model;
using System.Diagnostics;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

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
                return View("Modal/SaidaEstoque");
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
                return Json(new { locacao = peca.Locacao, marca = peca.Marca, modelo = peca.Modelo});
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

        //GERA RELATÓRIO EM PDF
        [HttpGet]
        public IActionResult GenerateReportPDF(string query, DateTime? startDate, DateTime? endDate, string filterType)
        {
            var pecas = estoqueService.GetAll(); // Recupera todos os registros

            if (!string.IsNullOrEmpty(query) || startDate.HasValue || endDate.HasValue)
            {
                pecas = estoqueService.SearchPecas(query, startDate, endDate);
            }

            IEnumerable<Peca> filteredPecas = pecas;

            //if (filterType == "data")
            //{
            //    filteredPecas = filteredPecas.OrderByDescending(c => c.Data);
            //}
            if (filterType == "CodigoSistema")
            {
                filteredPecas = filteredPecas.OrderBy(c => c.CodigoSistema);
            }
            else if (filterType == "Locacao")
            {
                filteredPecas = filteredPecas.OrderBy(c => c.Locacao);
            }
            else if (filterType == "Marca")
            {
                filteredPecas = filteredPecas.OrderBy(c => c.Marca);
            }
            else if (filterType == "Modelo")
            {
                filteredPecas = filteredPecas.OrderBy(c => c.Modelo);
            }
            else if (filterType == "Marca")
            {
                filteredPecas = filteredPecas.OrderBy(c => c.Quantidade);
            }
            else if (filterType == "Marca")
            {
                filteredPecas = filteredPecas.OrderByDescending(c => c.Data);
            }

            using (var memoryStream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                PdfPTable table = new PdfPTable(7);
                table.AddCell("Codigo no Sistema");
                table.AddCell("Locação");
                table.AddCell("Marca");
                table.AddCell("Modelo");
                table.AddCell("Quantidade");
                table.AddCell("Data da ultima entrada");

                foreach (var chamado in filteredPecas)
                {
                    table.AddCell(chamado.CodigoSistema);
                    table.AddCell(chamado.Locacao);
                    table.AddCell(chamado.Marca);
                    table.AddCell(chamado.Modelo);
                    table.AddCell(chamado.Quantidade.ToString());
                    table.AddCell(chamado.Data.ToString("dd/MM/yyyy"));
                }

                document.Add(table);
                document.Close();

                return File(memoryStream.ToArray(), "application/pdf", "RelatorioEstoque.pdf");
            }

        }

        //GERA RELATÓRIO EM EXCEL
        [HttpGet]
        public IActionResult GenerateReportExcel(string query, DateTime? startDate, DateTime? endDate, string filterType)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var pecas = estoqueService.GetAll();

            if (!string.IsNullOrEmpty(query) || startDate.HasValue || endDate.HasValue)
            {
                pecas = estoqueService.SearchPecas(query, startDate, endDate);
            }

            IEnumerable<Peca> filteredPecas = pecas;

            //if (filterType == "data")
            //{
            //    filteredPecas = filteredPecas.OrderByDescending(c => c.Data);
            //}
            if (filterType == "CodigoSistema")
            {
                filteredPecas = filteredPecas.OrderBy(c => c.CodigoSistema);
            }
            else if (filterType == "Locacao")
            {
                filteredPecas = filteredPecas.OrderBy(c => c.Locacao);
            }
            else if (filterType == "Marca")
            {
                filteredPecas = filteredPecas.OrderBy(c => c.Marca);
            }
            else if (filterType == "Modelo")
            {
                filteredPecas = filteredPecas.OrderBy(c => c.Modelo);
            }
            else if (filterType == "Marca")
            {
                filteredPecas = filteredPecas.OrderBy(c => c.Quantidade);
            }
            else if (filterType == "Marca")
            {
                filteredPecas = filteredPecas.OrderByDescending(c => c.Data);
            }

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("RelatorioEstoque");

                worksheet.Cells[1, 1].Value = "Codigo no Sistema";
                worksheet.Cells[1, 2].Value = "Locação";
                worksheet.Cells[1, 3].Value = "Marca";
                worksheet.Cells[1, 4].Value = "Modelo";
                worksheet.Cells[1, 5].Value = "Quantidade";
                worksheet.Cells[1, 6].Value = "Data";

                int row = 2;
                foreach (var peca in filteredPecas)
                {
                    worksheet.Cells[row, 2].Value = peca.CodigoSistema;
                    worksheet.Cells[row, 3].Value = peca.Locacao;
                    worksheet.Cells[row, 4].Value = peca.Marca;
                    worksheet.Cells[row, 5].Value = peca.Modelo;
                    worksheet.Cells[row, 6].Value = peca.Quantidade;
                    worksheet.Cells[row, 7].Value = peca.Data.ToString("dd/MM/yyyy");
                    row++;
                }

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);

                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RelatorioEstoque.xlsx");
            }
        }
    }
}


