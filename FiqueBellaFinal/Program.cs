using FiqueBellaFinal.Data;
using FiqueBellaFinal.Areas.Admin.Services;
using FiqueBellaFinal.Repositories;
using FiqueBellaFinal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Iniciando configuração do builder...");

// 🔴 Obrigatório no Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// 🔹 DbContext SQL Server com timeout aumentado
var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION") 
                       ?? "Server=tramway.proxy.rlwy.net,32176;Database=FiqueBellaDB;User Id=sa;Password=FiqueBella@2025;TrustServerCertificate=True;Encrypt=True;Connect Timeout=120;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

Console.WriteLine("DbContext adicionado.");

// 🔹 Repositórios
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IContabilidadeRepository, ContabilidadeRepository>();
builder.Services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();
builder.Services.AddScoped<ISugestaoRepository, SugestaoRepository>();

// 🔹 Serviços
builder.Services.AddScoped<RelatorioServices>();
builder.Services.AddScoped<GraficoServices>();

builder.Services.AddControllersWithViews()
       .AddRazorRuntimeCompilation();

builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap5";
});

var app = builder.Build();

Console.WriteLine("Iniciando teste de conexão com o banco...");

// 🔹 Retry de conexão com tempo maior
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    int retries = 5;
    int delay = 10000; // 10 segundos de espera entre tentativas

    for (int i = 0; i < retries; i++)
    {
        try
        {
            Console.WriteLine($"Tentativa {i + 1}/{retries}...");
            if (db.Database.CanConnect())
            {
                Console.WriteLine("Conexão OK. Aplicando migrations...");
                db.Database.Migrate();
                Console.WriteLine("Migrations aplicadas.");
                break;
            }
            else
            {
                Console.WriteLine("Banco indisponível no momento.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar ou migrar banco: {ex.Message}");
            if (i == retries - 1)
            {
                Console.WriteLine("Excedidas todas as tentativas. Continuando sem migrations.");
                throw;
            }
            Thread.Sleep(delay); // Aguardar mais tempo antes da próxima tentativa
        }
    }
}

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

Console.WriteLine($"Aplicação pronta. Rodando na porta {port}...");
app.Run();
