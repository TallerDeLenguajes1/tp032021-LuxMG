using CadeteriaWeb.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RepositorioCadeteSQL repoCadetes =
                    new RepositorioCadeteSQL(
                        Configuration.GetConnectionString("Default"));
            RepositorioClienteSQL repoClientes =
                    new RepositorioClienteSQL(
                        Configuration.GetConnectionString("Default"));
            RepositorioPedidoSQL repoPedidos =
                    new RepositorioPedidoSQL(
                        Configuration.GetConnectionString("Default"));
            RepositorioUsuarioSQL repoUsuarios =
                    new RepositorioUsuarioSQL(
                        Configuration.GetConnectionString("Default"));

            DataContext data = new DataContext(repoCadetes, repoClientes, repoPedidos, repoUsuarios);
            services.AddSingleton(data);

            services.AddAutoMapper(typeof(PerfilDeMapeo));
            services.AddSingleton(NLog.LogManager.GetCurrentClassLogger());
            services.AddControllersWithViews();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(60*60*24);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
