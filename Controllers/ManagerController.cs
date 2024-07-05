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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


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
            return View("Modal/LoginManager");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (login.Usuario == "admin" && login.Senha == "admin")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.Usuario),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("EstoqueIndex");
            }
            else
            {
                ViewBag.ErrorMessage = "Usuário ou senha inválidos.";
                return View("Modal/LoginManager");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("ModalLoginManager");
        }


        // Faz a abertura da view estoque para manager
        public IActionResult RegistroManager()
        {
            var model = registroService.GetAll();
            return View(model);
        }
    }
}
