using FiqueBellaFinal.Areas.Admin.Services;
using FiqueBellaFinal.Controllers;
using FiqueBellaFinal.Models;
using FiqueBellaFinal.Repositories;
using FiqueBellaFinal.Repositories.Interfaces;
using FiqueBellaFinal.Services;
using Microsoft.AspNetCore.HttpOverrides;
using ReflectionIT.Mvc.Paging;
using System.Diagnostics; // Se usar Activity



namespace FiqueBellaFinal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // ============================
        // SERVICES
        // ============================
        public void ConfigureServices(IServiceCollection services)
        {
            // 🔴 BANCO + IDENTITY DESATIVADOS TEMPORARIAMENTE

            services.AddTransient<IProcedimentoRepository, ProcedimentoRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IContabilidadeRepository, ContabilidadeRepository>();
            services.AddTransient<ISugestaoRepository, SugestaoRepository>();

            services.AddScoped<RelatorioServices>();
            services.AddScoped<RelatorioContabilidadeServices>();
            services.AddScoped<GraficoServices>();
            services.AddScoped<GaleriaController>();

            services.Configure<ConfigurationImagens>(
                Configuration.GetSection("ConfigurationPastaImagens")
            );

            services.AddControllersWithViews();

            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.PageParameterName = "pageIndex";
            });

            services.AddMemoryCache();
            services.AddSession();
        }

        // ============================
        // PIPELINE
        // ============================
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor |
                    ForwardedHeaders.XForwardedProto
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // ❌ SEM HTTPS INTERNO
            // app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
