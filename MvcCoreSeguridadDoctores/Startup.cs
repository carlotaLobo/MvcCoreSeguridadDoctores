using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCoreSeguridadDoctores.Data;
using MvcCoreSeguridadDoctores.Repositories;

namespace MvcCoreSeguridadDoctores
{
    public class Startup
    {
        IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
       
        public void ConfigureServices(IServiceCollection services)
        {
            // para hacerlo dinamico
            //TEMPDATA
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            services.AddSession();
            //AUTHENTICATION

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }
                      
            ).AddCookie() ;

            //CADENA Y DATOS
            String cadena = this.configuration.GetConnectionString("sql");
            services.AddDbContext<Context>(options => options.UseSqlServer(cadena));
            services.AddTransient<IRepository, Repository>();
            //VIEW AL CONTROLLERS TEMPDATA
            services.AddControllersWithViews(options=> 
                options.EnableEndpointRouting= false).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(rutes =>
            {
                rutes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
