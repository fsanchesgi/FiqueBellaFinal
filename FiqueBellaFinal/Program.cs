using Microsoft.EntityFrameworkCore;
using FiqueBellaFinal.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura DbContext (se você quiser manter a API de produtos)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adiciona suporte a MVC (Controllers + Views)
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Rota padrão MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Rotas da API (opcional)
app.MapControllers();

app.Run();
