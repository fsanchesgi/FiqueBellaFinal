using Microsoft.EntityFrameworkCore;
using FiqueBellaFinal.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura DbContext (se usado)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// NÃO USE HTTPS no Railway
// app.UseHttpsRedirection();  <-- remova ou comente

// Middleware para arquivos estáticos
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Rotas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Rotas API
app.MapControllers();

// Porta dinâmica do Railway (obrigatório)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

app.Run();
