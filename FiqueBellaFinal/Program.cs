using FiqueBellaFinal.Data;
using FiqueBellaFinal.Areas.Admin.Services;
using FiqueBellaFinal.Repositories;
using FiqueBellaFinal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using Npgsql;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Iniciando configuração do builder...");

// 🔴 Obrigatório no Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// 🔹 Obter connection string do Railway
string connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

// 🔹 Converter DATABASE_URL do formato postgresql:// para Npgsql
if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("postgresql://"))
{
    // remover "postgresql://"
    connectionString = connectionString.Replace("postgresql://", "");
    
    var userPassAndHost = connectionString.Split('@');
    var userPass = userPassAndHost[0].Split(':');
    var hostAndDb = userPassAndHost[1].Split('/');
    var hostPort = hostAndDb[0].Split(':');

    var builderNpgsql = new NpgsqlConnectionStringBuilder
    {
        Host = hostPort[0],
        Port = int.Parse(hostPort[1]),
        Username = userPass[0],
        Password = userPass[1],
        Database = hostAndDb[1],
        SslMode = SslMode.Prefer,
        TrustServerCertificate = true,
        Timeout = 120
    };

    connectionString = builderNpgsql.ConnectionString;
}

// 🔹 Fallback caso DATABASE_URL não esteja definida (local)
if (string.IsNullOrEmpty(connectionString))
{
    connectionString = "Host=postgres.railway.internal;Port=5432;Database=railway;Username=postgres;Password=FiqueBella2025;SSL Mode=Prefer;Trust Server Certificate=true;";
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

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
    int delay = 15000; // 15 segundos

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
            Thread.Sleep(delay);
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
