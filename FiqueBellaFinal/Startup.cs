using FiqueBellaFinal.Areas.Admin.Services;
using FiqueBellaFinal.Context;
using FiqueBellaFinal.Repositories;
using FiqueBellaFinal.Repositories.Interfaces;
using FiqueBellaFinal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace FiqueBellaFinal;
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
        services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DataBase")));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IProcedimentoRepository, ProcedimentoRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        services.AddScoped<RelatorioServices>();
        services.AddScoped<GraficoServices>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin",
                politica =>
                {
                    politica.RequireRole("Admin");
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

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
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
        app.UseSession();
        app.UseRouting();

        seedUserRoleInitial.SeedRoles();
        seedUserRoleInitial.SeedUser();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
               name: "categoriaFiltro",
               pattern: "Procedimento/{action}/{categoria?}",
               defaults: new { Controller = "Procedimento", action = "List" });

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}