using FiqueBellaFinal.Data;
using FiqueBellaFinal.Areas.Admin.Services;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Iniciando configuração do builder...");

// Adiciona o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null
            );
        }
    )
);

Console.WriteLine("DbContext adicionado.");

// Serviços customizados
builder.Services.AddScoped<RelatorioServices>();
builder.Services.AddScoped<GraficoServices>();

// Controllers com views e runtime compilation
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();

// ReflectionIT.Mvc.Paging
builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap5";
});

var app = builder.Build();

Console.WriteLine("Builder finalizado. Iniciando teste de conexão com o banco...");

// --- TESTE DE CONEXÃO COM O BANCO ---
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        Console.WriteLine("Tentando conectar ao banco...");

        if (db.Database.CanConnect())
        {
            Console.WriteLine("Conexão com o banco de dados OK!");

            Console.WriteLine("Tentando aplicar migrations...");
            db.Database.Migrate();
            Console.WriteLine("Migrations aplicadas com sucesso!");
        }
        else
        {
            Console.WriteLine("Banco indisponível no momento. Aplicação continuará sem migrations.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao conectar ou aplicar migrations:");
        Console.WriteLine(ex.Message);
        Console.WriteLine("Aplicação continuará rodando mesmo sem banco.");
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
