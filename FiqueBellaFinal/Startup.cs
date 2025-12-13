using FiqueBellaFinal.Areas.Admin.Services;
using FiqueBellaFinal.Context;
using FiqueBellaFinal.Controllers;
using FiqueBellaFinal.Models;
using FiqueBellaFinal.Repositories;
using FiqueBellaFinal.Repositories.Interfaces;
using FiqueBellaFinal.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

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
            // 🔴 COMENTADO TEMPORARIAMENTE PARA TESTE
            /*
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DataBase")
                ));
            */

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IProcedimentoRepository, ProcedimentoRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IContabilidadeRepository, ContabilidadeRepository>();
            services.AddTransient<ISugestaoRepository, SugestaoRepository>();

            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            services.AddScoped<RelatorioServices>();
            services.AddScoped<RelatorioContabilidadeServices>();
            services.AddScoped<GraficoServices>();
            services.AddScoped<GaleriaController>();

            services.Configure<ConfigurationImagens>(
                Configuration.GetSection("ConfigurationPastaImagens")
            );

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireRole("Admin");
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ISeedUserRoleInitial seedUserRoleInitial)
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

            // ❌ NÃO USAR HTTPS INTERNAMENTE NO RAILWAY
            // app.UseHttpsRedirection();
            // app.UseHsts();

            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            // 🔴 COMENTADO TEMPORARIAMENTE
            // seedUserRoleInitial.SeedRoles();
            // seedUserRoleInitial.SeedUser();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "categoriaFiltro",
                    pattern: "Procedimento/{action}/{categoria?}",
                    defaults: new
                    {
                        Controller = "Procedimento",
                        action = "List"
                    }
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
