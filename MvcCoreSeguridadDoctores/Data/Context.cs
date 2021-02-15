using Microsoft.EntityFrameworkCore;
using MvcCoreSeguridadDoctores.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreSeguridadDoctores.Data
{
    public class Context:DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Doctor> Doctores { get; set; }
    }
}
