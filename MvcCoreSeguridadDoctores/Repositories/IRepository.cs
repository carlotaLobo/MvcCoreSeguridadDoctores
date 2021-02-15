using MvcCoreSeguridadDoctores.Data;
using MvcCoreSeguridadDoctores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreSeguridadDoctores.Repositories
{
    public interface IRepository
    {
        List<Doctor> GetDoctoresEspecialidad(String especialidad);
        Doctor ExisteDoctor(String Apellido, int iddoctor);
        Doctor GetDoctor(int id);
    }
}
