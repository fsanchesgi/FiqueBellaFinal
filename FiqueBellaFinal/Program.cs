using FiqueBellaFinal.Repositories;
using FiqueBellaFinal.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços MVC
builder.Services.AddControllersWithViews();

// Registra repositório
builder.Services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // serve wwwroot

app.UseRouting();
app.UseAuthorization();

// Rotas padrão MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
