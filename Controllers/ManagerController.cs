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

        [HttpGet]
        public IActionResult LoginManager()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (login.Usuario == "admin" && login.Senha == "pal4568#")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.Usuario),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("EstoqueIndex", "Estoque");
            }
            else
            {
                ViewBag.ErrorMessage = "Usuário ou senha inválidos.";
                return View("LoginManager");
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LoginManager");
        }



    }
}
