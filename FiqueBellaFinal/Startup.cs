using FiqueBellaFinal.Data;
using FiqueBellaFinal.Areas.Admin.Services;
using FiqueBellaFinal.Repositories;
using FiqueBellaFinal.Repositories.Interfaces;
using FiqueBellaFinal.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using System;
using System.Threading;

namespace FiqueBellaFinal
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // 🔹 DbContext SQL Server
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // 🔹 Repositórios
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IContabilidadeRepository, ContabilidadeRepository>();
            services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();
            services.AddScoped<ISugestaoRepository, SugestaoRepository>();

            // 🔹 Serviços
            services.AddScoped<RelatorioServices>();
            services.AddScoped<GraficoServices>();

            // 🔹 Controllers + Razor runtime
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            // 🔹 Paging
            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap5";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // 🔹 Teste de conexão com retry e migrations
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                int retries = 5;
                int delay = 10000; // 10 segundos

                for (int i = 0; i < retries; i++)
                {
                    try
                    {
                        Console.WriteLine($"Tentativa {i + 1}/{retries} para conectar ao banco...");

                        if (db.Database.CanConnect())
                        {
                            Console.WriteLine("Conexão com banco OK. Aplicando migrations...");
                            db.Database.Migrate();
                            Console.WriteLine("Migrations aplicadas.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Banco indisponível no momento.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao conectar ou migrar banco (tentativa {i + 1}/{retries}): {ex.Message}");
                        if (i == retries - 1)
                        {
                            Console.WriteLine("Excedidas todas as tentativas. Continuando sem migrations.");
                            throw;
                        }
                        Thread.Sleep(delay);
                    }
                }
            }

            // 🔹 Rotas
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
