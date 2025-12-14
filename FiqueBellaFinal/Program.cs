using FiqueBellaFinal.Data;
using FiqueBellaFinal.Areas.Admin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReflectionIT.Mvc.Paging;
using System.Diagnostics; // Se usar Activity
using FiqueBellaFinal.Context;



var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona serviços customizados
builder.Services.AddScoped<RelatorioServices>();
builder.Services.AddScoped<GraficoServices>();

// Adiciona controllers com views e runtime compilation
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// Adiciona ReflectionIT.Mvc.Paging atualizado
builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap5";
});

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
