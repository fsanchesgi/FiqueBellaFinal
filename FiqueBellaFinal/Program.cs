using FiqueBellaFinal.Data;
using FiqueBellaFinal.Areas.Admin.Services;
using FiqueBellaFinal.Repositories;
using FiqueBellaFinal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Threading;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Iniciando configuração do builder...");

// 🔴 Obrigatório no Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// 🔹 Configurando DbContext PostgreSQL com DATABASE_URL do Railway
string connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

// Se a connection string vier no formato URL do Railway, converte manualmente
if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("postgresql://"))
{
    // Remove prefixo
    var cleanUrl = connectionString.Replace("postgresql://", "");

    // Divide user:pass e host:port/db
    var atIndex = cleanUrl.IndexOf('@');
    var userPass = cleanUrl.Substring(0, atIndex);
    var hostDb = cleanUrl.Substring(atIndex + 1);

    var colonIndex = userPass.IndexOf(':');
    var username = userPass.Substring(0, colonIndex);
    var password = userPass.Substring(colonIndex + 1);

    var slashIndex = hostDb.IndexOf('/');
    var hostPort = hostDb.Substring(0, slashIndex);
    var database = hostDb.Substring(slashIndex + 1);

    var colonHost = hostPort.IndexOf(':');
    var host = hostPort.Substring(0, colonHost);
    var portDb = int.Parse(hostPort.Substring(colonHost + 1));

    var npgsqlBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = host,
        Port = portDb,
        Username = username,
        Password = password,
        Database = database,
        SslMode = SslMode.Prefer,
        TrustServerCertificate = true,
        Timeout = 120
    };

    connectionString = npgsqlBuilder.ConnectionString;
}

// Fallback caso DATABASE_URL não esteja definida
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
    int delay = 15000; // 15 segundos de espera entre tentativas

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
