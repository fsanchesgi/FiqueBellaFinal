using FiqueBellaFinal.Data; // mantido apenas uma vez
using FiqueBellaFinal.Areas.Admin.Services;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Diagnostics; // se usar Activity

var builder = WebApplication.CreateBuilder(args);

// Adiciona o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Serviços customizados
builder.Services.AddScoped<RelatorioServices>();
builder.Services.AddScoped<GraficoServices>();

// Controllers com views e runtime compilation
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// ReflectionIT.Mvc.Paging atualizado
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
