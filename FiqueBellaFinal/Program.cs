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

// 🔴 Porta obrigatória no Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// 🔹 Conexão com PostgreSQL
// Usamos DATABASE_URL se disponível, mas sem parsing de URI
string connectionString = Environment.GetEnvironmentVariable("DATABASE_URL_FALLBACK") 
    ?? "Host=postgres.railway.internal;Port=5432;Database=railway;Username=postgres;Password=FiqueBella2025;SSL Mode=Prefer;Trust Server Certificate=true;";

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

// 🔹 Retry de conexão
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    int retries = 5;
    int delay = 15000; // 15s

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
            if (i == retries - 1) throw;
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
app.UseAuthenticati
