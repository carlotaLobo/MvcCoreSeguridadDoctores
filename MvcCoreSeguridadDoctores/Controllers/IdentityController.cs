using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcCoreSeguridadDoctores.Models;
using MvcCoreSeguridadDoctores.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcCoreSeguridadDoctores.Controllers
{
    public class IdentityController : Controller
    {
        IRepository repository;
        public IdentityController(IRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(String username, int password)
        {
            Doctor doctor = this.repository.ExisteDoctor(username, password);
            if (doctor == null)
            {
                ViewBag.message = "password o username es incorrecto";
                return View();
            }

            ClaimsIdentity identity = new ClaimsIdentity(
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimTypes.Name, ClaimTypes.Role
                );
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, doctor.NumeroDoctor.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, doctor.Apellido));
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddMinutes(15)
                }
                );
            String action = TempData["action"].ToString();
            String controller = TempData["controller"].ToString();

            return RedirectToAction(action, controller);

        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index","home");
        }
    }
}
