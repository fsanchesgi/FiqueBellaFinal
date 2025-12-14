using FiqueBellaFinal.Data;
using FiqueBellaFinal.Areas.Admin.Services;
using FiqueBellaFinal.Repositories;
using FiqueBellaFinal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Iniciando configuração do builder...");

// 🔴 OBRIGATÓRIO NO RAILWAY
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// 🔹 FORÇAR TLS 1.2 (importante para Railway)
System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
AppContext.SetSwitch("System.Net.Sockets.EnableMultipleTcpConnections", true);

// 🔹 STRING DE CONEXÃO COM SQL SERVER
var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION") 
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");

// 🔹 DbContext atualizado para SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

Console.WriteLine("DbContext adicionado.");

// 🔹 REGISTRO DE TODOS OS REPOSITÓRIOS
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IContabilidadeRepository, ContabilidadeRepository>();
builder.Services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();
builder.Services.AddScoped<ISugestaoRepository, SugestaoRepository>();

// 🔹 Serviços
builder.Services.AddScoped<RelatorioServices>();
builder.Services.AddScoped<GraficoServices>();

// 🔹 Controllers com views
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// 🔹 Paging
builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap5";
});

var app = builder.Build();

Console.WriteLine("Builder finalizado. Iniciando teste de conexão com o banco...");

// 🔹 TESTE DE CONEXÃO COM RETRY (SEM DERRUBAR A APP)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    int retries = 5;
    for (int i = 0; i < retries; i++)
    {
        try
        {
            Console.WriteLine("Tentando conectar ao banco...");

            if (db.Database.CanConnect())
            {
                Console.WriteLine("Conexão com banco OK. Aplicando migrations...");
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
            Console.WriteLine($"Erro ao conectar ou migrar banco (tentativa {i + 1}/{retries}): {ex.Message}");
            if (i == retries - 1)
            {
                Console.WriteLine("Excedidas as tentativas de conexão. A aplicação continuará sem migrations.");
                throw;
            }
            Thread.Sleep(5000); // Aguardar 5 segundos antes de tentar novamente
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

// 🔹 Rotas
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Console.WriteLine($"Aplicação pronta. Rodando na porta {port}...");
app.Run();
