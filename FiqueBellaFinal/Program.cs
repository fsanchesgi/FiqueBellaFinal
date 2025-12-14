using FiqueBellaFinal.Data;
using FiqueBellaFinal.Areas.Admin.Services;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Iniciando configuração do builder...");

// 🔴 OBRIGATÓRIO NO RAILWAY
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

Console.WriteLine("DbContext adicionado.");

// Serviços
builder.Services.AddScoped<RelatorioServices>();
builder.Services.AddScoped<GraficoServices>();

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap5";
});

var app = builder.Build();

Console.WriteLine("Builder finalizado. Iniciando teste de conexão com o banco...");

// 🔹 TESTE DE CONEXÃO (SEM DERRUBAR A APP)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        Console.WriteLine("Tentando conectar ao banco...");

        if (db.Database.CanConnect())
        {
            Console.WriteLine("Conexão com banco OK. Aplicando migrations...");
            db.Database.Migrate();
            Console.WriteLine("Migrations aplicadas.");
        }
        else
        {
            Console.WriteLine("Banco indisponível no momento. Aplicação continuará sem migrations.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao conectar ou migrar banco: " + ex.Message);
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
