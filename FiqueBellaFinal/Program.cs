using Microsoft.EntityFrameworkCore;
using FiqueBellaFinal.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura DbContext (opcional, se usar SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Porta dinâmica do Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Urls.Add($"http://*:{port}");

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// ❌ Remover HTTPS no Railway
// app.UseHttpsRedirection();

// Servir arquivos estáticos
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Rotas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Rotas API
app.MapControllers();

app.Run();
