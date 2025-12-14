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
using Microsoft.Extensions.Logging;
using ReflectionIT.Mvc.Paging;
using System;
using System.Threading.Tasks;

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
            // 🔹 DbContext PostgreSQL
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
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

            // 🔹 Teste de conexão com retry exponencial e migrations
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                int retries = 5;
                int delay = 5000; // 5 segundos inicial

                Task.Run(async () =>
                {
                    for (int i = 0; i < retries; i++)
                    {
                        try
                        {
                            logger.LogInformation($"Tentativa {i + 1}/{retries} para conectar ao banco...");
                            if (await db.Database.CanConnectAsync())
                            {
                                logger.LogInformation("Conexão com banco OK. Aplicando migrations...");
                                await db.Database.MigrateAsync();
                                logger.LogInformation("Migrations aplicadas com sucesso.");
                                break;
                            }
                            else
                            {
                                logger.LogWarning("Banco indisponível no momento.");
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"Erro ao conectar ou migrar banco (tentativa {i + 1}/{retries}).");
                            if (i == retries - 1)
                            {
                                logger.LogError("Excedidas todas as tentativas. Continuando sem migrations.");
                            }
                        }

                        await Task.Delay(delay);
                        delay *= 2; // exponencial backoff
                    }
                }).GetAwaiter().GetResult(); // bloqueia até terminar as tentativas
            }

            // 🔹 Rotas
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
