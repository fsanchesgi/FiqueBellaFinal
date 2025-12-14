using FiqueBellaFinal.Data; // mantido apenas uma vez
using FiqueBellaFinal.Areas.Admin.Services;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Diagnostics; // se usar Activity

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Iniciando configuração do builder...");

// Adiciona o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
Console.WriteLine("DbContext adicionado.");

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
Console.WriteLine("Builder finalizado. Iniciando teste de conexão com o banco...");

// --- TESTE DE CONEXÃO COM O BANCO ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        Console.WriteLine("Tentando conectar ao banco...");
        if (!db.Database.CanConnect())
        {
            Console.WriteLine("Não foi possível conectar ao banco de dados.");
        }
        else
        {
            Console.WriteLine("Conexão com o banco de dados OK!");
        }

        Console.WriteLine("Aplicando migrations...");
        db.Database.Migrate();
        Console.WriteLine("Migrations aplicadas com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro de conexão ou migração: " + ex.Message);
        throw; // mantém a exceção para aparecer no log do Railway
    }
}
// --- FIM DO TESTE ---

Console.WriteLine("Pipeline HTTP iniciando...");

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

Console.WriteLine("Aplicação pronta. Rodando...");
app.Run();
