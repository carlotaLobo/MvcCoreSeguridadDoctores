using MvcCoreSeguridadDoctores.Data;
using MvcCoreSeguridadDoctores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreSeguridadDoctores.Repositories
{
    public class Repository:IRepository
    {
        Context context;

        public Repository(Context context)
        {
            this.context = context;
        }

        public Doctor ExisteDoctor(string Apellido, int iddoctor)
        {
            var consulta = from datos in this.context.Doctores
                           where datos.Apellido == Apellido
                            && datos.NumeroDoctor == iddoctor
                           select datos;
            return consulta.FirstOrDefault();
        }

        public Doctor GetDoctor(int id)
        {
            return this.context.Doctores.Where(z=>z.NumeroDoctor == id).FirstOrDefault();
        }

        public List<Doctor> GetDoctoresEspecialidad(String especialidad)
        {
            return this.context.Doctores.Where(x => x.Especialidad == especialidad).ToList();
        }
    }
}
