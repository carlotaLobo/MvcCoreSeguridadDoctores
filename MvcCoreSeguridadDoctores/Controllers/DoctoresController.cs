using Microsoft.AspNetCore.Mvc;
using MvcCoreSeguridadDoctores.Filters;
using MvcCoreSeguridadDoctores.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcCoreSeguridadDoctores.Controllers
{
    [AuthorizeDoctores]
    public class DoctoresController : Controller
    {
        IRepository repository;
        public DoctoresController(IRepository repository)
        {
            this.repository = repository;
        }
      
        public IActionResult Perfil()
        {
            String dato = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            return View(this.repository.GetDoctor(Convert.ToInt32(dato)));
        }
   
        public IActionResult DoctoresEspecialidad()
        {
            String dato = User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
            return View(
                this.repository.GetDoctoresEspecialidad(
                    this.repository.GetDoctor(Convert.ToInt32(dato)
                    ).Especialidad)
                );
        }
    }
}
